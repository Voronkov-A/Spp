using IntegrationMocks.Core;
using IntegrationMocks.Core.Networking;
using Spp.Common.Miscellaneous;
using Spp.IdentityProvider.TestClient.Auth.V1;
using Spp.IdentityProvider.TestClient.Connect.V1;
using Spp.IdentityProvider.Tests.TestHelpers;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IntegrationMocks.Core.Names;
using IntegrationMocks.Modules.Sql;
using IntegrationMocks.Modules.Yugabyte;
using Xunit;
using AutoFixture;
using Spp.IdentityProvider.Domain.Users;
using Spp.IdentityProvider.Application.Users.Settings;
using Spp.Common.TestHelpers.Http;
using Spp.IdentityProvider.WebApi.Auth;

namespace Spp.IdentityProvider.Tests.Service;

public class SampleClientApplicationTests : IClassFixture<SampleClientApplicationTests.GlobalState>
{
    private readonly IFixture _fixture;
    private readonly IInfrastructureService<IdentityProviderContract> _identityProvider;
    private readonly IInfrastructureService<SampleClientApplicationContract> _sampleClientApplication;
    private readonly DefaultUserSettings _superuser;

    public SampleClientApplicationTests(GlobalState state)
    {
        _fixture = new Fixture();
        _identityProvider = state.IdentityProvider;
        _sampleClientApplication = state.SampleClientApplication;
        _superuser = _identityProvider.Contract.Configuration.DefaultUserSet.Users.First();
    }

    [Fact]
    public async Task Cannot_access_resource_when_unauthorized()
    {
        // ARRANGE
        using var client = new SampleClientApplicationClient(HttpClientFactory.Create(
            _sampleClientApplication.Contract.WebApiUrl));

        // ACT
        var statusCode = await client.Resource();

        // ASSERT
        Assert.Equal(HttpStatusCode.Unauthorized, statusCode);
    }

    [Fact]
    public async Task Can_access_resource_when_authorized_as_identity_provider_user()
    {
        // ARRANGE
        using var authHttpClient = HttpClientFactory.Create(_identityProvider.Contract.WebApiUrl);
        using var authClient = new AuthTestClient(authHttpClient);
        (await authClient
            .SignIn(new SignInRequest(_superuser.Username, _superuser.Password, Scopes.Api)))
            .EnsureStatusCode(HttpStatusCode.NoContent);
        var authenticationAccessToken = authHttpClient.Handler.CookieContainer
            .GetAllCookies()
            .First(x => x.Name == AccessTokenCookie.Name)
            .Value;
        using var connectClient = new ConnectTestClient(HttpClientFactory.Create(
            _identityProvider.Contract.WebApiUrl,
            authenticationAccessToken));

        using var httpClient = HttpClientFactory.Create(_sampleClientApplication.Contract.WebApiUrl);
        using var client = new SampleClientApplicationClient(httpClient);

        var uiRedirectUri = _fixture.Create<Uri>();
        var authorizeUri = await client.GetSignInRedirectUri(uiRedirectUri);
        using var authorizeRequest = new HttpRequestMessage(HttpMethod.Get, authorizeUri);
        var authorizeResponse = await connectClient.Authorize(authorizeRequest);
        var callbackRedirectUri = await client.GetCallbackRedirectUri(new Uri(authorizeResponse.Headers302.Location));

        // ACT
        var statusCode = await client.Resource();

        // ASSERT
        Assert.Equal(HttpStatusCode.NoContent, statusCode);
        Assert.Equal(uiRedirectUri, new Uri(callbackRedirectUri));
    }

    public class GlobalState : IAsyncLifetime
    {
        public GlobalState()
        {
            var portManager = PortManager.Default;
            var nameGenerator = new RandomNameGenerator(nameof(SampleClientApplicationTests));
            Yugabyte = new DockerYugabyteService(nameGenerator, portManager);
            IdentityProvider = new IdentityProviderService(nameGenerator, portManager, Yugabyte);
            SampleClientApplication = new SampleClientApplicationService(portManager, IdentityProvider);
        }

        public IInfrastructureService<SqlServiceContract> Yugabyte { get; }

        public IInfrastructureService<IdentityProviderContract> IdentityProvider { get; }

        public IInfrastructureService<SampleClientApplicationContract> SampleClientApplication { get; }

        public async Task DisposeAsync()
        {
            await DisposableUtils.Dispose(SampleClientApplication);
            await DisposableUtils.Dispose(IdentityProvider);
            await DisposableUtils.Dispose(Yugabyte);
        }

        public async Task InitializeAsync()
        {
            await Yugabyte.InitializeAsync();
            await IdentityProvider.InitializeAsync();
            await SampleClientApplication.InitializeAsync();
        }
    }
}
