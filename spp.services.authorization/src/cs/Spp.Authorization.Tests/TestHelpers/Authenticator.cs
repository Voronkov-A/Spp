using IntegrationMocks.Core;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using Spp.Authorization.Application.Auth.Settings;
using Spp.Authorization.Domain.Users;
using Spp.Authorization.TestClient.Connect.V1;
using Spp.Authorization.Tests.TestHelpers.Services;
using Spp.Common.TestHelpers.Http;
using Spp.IdentityProvider.Client.Auth.V1;
using Spp.IdentityProvider.TestServer;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Spp.Authorization.TestClient.Auth.V1;
using Spp.Authorization.TestClient.Callback.V1;
using Spp.Common.Authentication;

namespace Spp.Authorization.Tests.TestHelpers;

internal class Authenticator(
    IInfrastructureService<AuthorizationContract> authorization,
    IInfrastructureService<IdentityProviderContract> identityProvider)
{
    private readonly AuthenticationSettings _authenticationSettings
        = authorization.Contract.Configuration.Authentication;

    public async Task<string> GetClientAccessToken(CancellationToken cancellationToken = default)
    {
        using var connectClient = new ConnectTestClient(authorization.Contract.WebApiUrl);
        var tokenData = (await connectClient.RequestAuthorizationCodeToken(
            clientId: _authenticationSettings.ClientId,
            grantType: GrantType.ClientCredentials,
            clientSecret: _authenticationSettings.ClientSecret,
            scope: Scopes.Api,
            cancellationToken: cancellationToken)).Content200;
        return tokenData.AccessToken;
    }

    public async Task<string> GetSuperuserAccessToken(CancellationToken cancellationToken = default)
    {
        var superuserSubjectId = authorization.Contract.Configuration.SuperuserSet.Items
            .First().Identities
            .First().SubjectId;
        var superuser = identityProvider.Contract.Configuration.DefaultUserSet.Users
            .First(x => x.DefaultId == superuserSubjectId);
        return await GetUserAccessToken(superuser.Username, superuser.Password, cancellationToken);
    }

    public async Task<string> GetUserAccessToken(CancellationToken cancellationToken = default)
    {
        var superuserSubjectId = authorization.Contract.Configuration.SuperuserSet.Items
            .First().Identities
            .First().SubjectId;
        var user = identityProvider.Contract.Configuration.DefaultUserSet.Users
            .First(x => x.DefaultId != superuserSubjectId);
        return await GetUserAccessToken(user.Username, user.Password, cancellationToken);
    }

    public async Task<string> GetUserAccessToken(
        string username,
        string password,
        CancellationToken cancellationToken = default)
    {
        using var identityProviderAuthHttpClient = HttpClientFactory.Create(identityProvider.Contract.WebApiUrl);
        using var identityProviderAuthClient = new AuthClient(
            identityProviderAuthHttpClient,
            new OptionsWrapper<JsonOptions>(new JsonOptions()));
        await identityProviderAuthClient.SignIn(new SignInRequest(username, password, "api"), cancellationToken);
        var authenticationAccessToken = identityProviderAuthHttpClient.Handler.CookieContainer.GetAllCookies()
            .First(x => x.Name == "A").Value;

        using var identityProviderConnectClient = new IdentityProvider.TestClient.Connect.V1.ConnectTestClient(
            HttpClientFactory.Create(identityProvider.Contract.WebApiUrl, authenticationAccessToken));

        var signInCookies = new CookieContainer();
        using var authHttpClient = new HttpClient(
            new HttpClientHandler
            {
                AllowAutoRedirect = false,
                CookieContainer = signInCookies
            });
        authHttpClient.BaseAddress = authorization.Contract.WebApiUrl;
        using var authClient = new AuthTestClient(authHttpClient);

        const string uiRedirectUri = "http://localhost";
        var authorizeUri = (await authClient.SignInWithIndentityProvider(uiRedirectUri, cancellationToken))
            .Headers302
            .Location;
        using var authorizeRequest = new HttpRequestMessage(HttpMethod.Get, authorizeUri);
        var authorizeResponse = await identityProviderConnectClient.Authorize(authorizeRequest, cancellationToken);

        var callbackCookies = new CookieContainer();
        using var callbackHttpClient = new HttpClient(
            new HttpClientHandler
            {
                AllowAutoRedirect = false,
                CookieContainer = callbackCookies
            });
        callbackHttpClient.BaseAddress = authorization.Contract.WebApiUrl;
        foreach (Cookie cookie in signInCookies.GetAllCookies())
        {
            callbackHttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Cookie", $"{cookie.Name}={cookie.Value}");
        }
        using var callbackClient = new CallbackTestClient(callbackHttpClient);
        using var callbackRequest = new HttpRequestMessage(HttpMethod.Get, authorizeResponse.Headers302.Location);
        _ = (await callbackClient.Callback(callbackRequest, cancellationToken)).Headers302;
        return callbackCookies.GetAllCookies().First(x => x.Name == AccessTokenCookie.Name).Value;
    }
}
