using AutoFixture;
using Duende.IdentityServer.Models;
using IdentityModel;
using IntegrationMocks.Core;
using Spp.Common.Errors.V1;
using Spp.Common.Miscellaneous;
using Spp.Common.TestHelpers.Http;
using Spp.IdentityProvider.Application.Applications.Settings;
using Spp.IdentityProvider.Application.Users.Settings;
using Spp.IdentityProvider.Domain.Users;
using Spp.IdentityProvider.TestClient.Auth.V1;
using Spp.IdentityProvider.TestClient.Connect.V1;
using Spp.IdentityProvider.TestClient.Users.V1;
using Spp.IdentityProvider.Tests.TestHelpers;
using Spp.IdentityProvider.WebApi.Auth;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xunit;
using GrantType = Spp.IdentityProvider.TestClient.Connect.V1.GrantType;

namespace Spp.IdentityProvider.Tests.Service;

[Collection(nameof(IdentityProviderServiceFixture))]
public class IdentityProviderUsersTests : IClassFixture<IdentityProviderServiceFixture>
{
    private readonly IInfrastructureService<IdentityProviderContract> _identityProvider;
    private readonly IFixture _fixture;
    private readonly DefaultApplicationSettings _defaultApplication;
    private readonly DefaultUserSettings _superuser;

    public IdentityProviderUsersTests(IdentityProviderServiceFixture identityProviderFixture)
    {
        _identityProvider = identityProviderFixture.IdentityProvider;
        _fixture = new Fixture();
        _defaultApplication = _identityProvider.Contract.Configuration.DefaultApplication;
        _superuser = _identityProvider.Contract.Configuration.DefaultUserSet.Users.First();
    }

    [Fact]
    public async Task Cannot_create_user_when_unauthorized()
    {
        // ARRANGE
        using var client = new UsersTestClient(_identityProvider.Contract.WebApiUrl);
        var request = _fixture.Create<CreateUserRequest>();

        // ACT
        var response = await client.Create(request);

        // ASSERT
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.Contains(EnumSerializer.ToString(CommonErrorCode.CommonAuthenticationFailure), response.Content401.Type);
    }

    [Fact]
    public async Task Can_create_user_when_authorized_as_client()
    {
        // ARRANGE
        using var connectClient = new ConnectTestClient(_identityProvider.Contract.WebApiUrl);
        var tokenData = (await connectClient.RequestAuthorizationCodeToken(
            clientId: _defaultApplication.ClientId,
            grantType: GrantType.ClientCredentials,
            clientSecret: _defaultApplication.ClientSecret,
            scope: Scopes.Api,
            redirectUri: null,
            code: null,
            codeVerifier: null,
            refreshToken: null)).Content200;

        using var client = new UsersTestClient(HttpClientFactory.Create(
            _identityProvider.Contract.WebApiUrl,
            tokenData.AccessToken));
        var request = _fixture.Create<CreateUserRequest>();

        // ACT
        var response = (await client.Create(request)).Content201;

        // ASSERT
        Assert.NotNull(response.Id);
    }

    [Fact]
    public async Task Can_create_user_when_authorized_as_superuser_by_login()
    {
        // ARRANGE
        using var authHttpClient = HttpClientFactory.Create(_identityProvider.Contract.WebApiUrl);
        using var authClient = new AuthTestClient(authHttpClient);
        (await authClient
            .SignIn(new SignInRequest(_superuser.Username, _superuser.Password, Scopes.All)))
            .EnsureStatusCode(HttpStatusCode.NoContent);
        var accessToken = authHttpClient.Handler.CookieContainer
            .GetAllCookies()
            .First(x => x.Name == AccessTokenCookie.Name)
            .Value;

        using var client = new UsersTestClient(HttpClientFactory.Create(
            _identityProvider.Contract.WebApiUrl,
            accessToken));
        var request = _fixture.Create<CreateUserRequest>();

        // ACT
        var response = (await client.Create(request)).Content201;

        // ASSERT
        Assert.NotNull(response.Id);
    }

    [Fact]
    public async Task Can_create_user_when_authorized_as_superuser_by_authorization_code()
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

        var callbackUri = _defaultApplication.RedirectUris.First().ToString();
        var requestedScope = Scopes.All;
        var code = _fixture.Create<Code>();

        var authorizeResponse = await connectClient.Authorize(
            clientId: _defaultApplication.ClientId,
            responseType: ResponseType.Code,
            redirectUri: callbackUri,
            scope: requestedScope,
            codeChallenge: code.Challenge,
            responseMode: ResponseMode.Query,
            codeChallengeMethod: CodeChallengeMethod.S256);

        var authorizationCode = HttpUtility.ParseQueryString(
            new Uri(authorizeResponse.Headers302.Location).Query)
            ["code"];

        var tokenData = (await connectClient.RequestAuthorizationCodeToken(
            clientId: _defaultApplication.ClientId,
            grantType: GrantType.AuthorizationCode,
            clientSecret: _defaultApplication.ClientSecret,
            scope: requestedScope,
            redirectUri: callbackUri,
            code: authorizationCode,
            codeVerifier: code.Verifier,
            refreshToken: null)).Content200;

        using var client = new UsersTestClient(HttpClientFactory.Create(
            _identityProvider.Contract.WebApiUrl,
            tokenData.AccessToken));

        var request = _fixture.Create<CreateUserRequest>();

        // ACT
        var response = (await client.Create(request)).Content201;

        // ASSERT
        Assert.NotNull(response.Id);
    }

    private class Code([StringLength(128, MinimumLength = 128)] string verifier)
    {
        public string Verifier { get; } = verifier;

        public string Challenge { get; } = CreateCodeChallenge(verifier);

        private static string CreateCodeChallenge(string codeVerifier)
        {
            var codeVerifierBytes = Encoding.ASCII.GetBytes(codeVerifier);
            var hashedBytes = codeVerifierBytes.Sha256();
            return Base64Url.Encode(hashedBytes);
        }
    }
}
