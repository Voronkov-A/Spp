using System;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Spp.Common.Authentication.Abstractions;

namespace Spp.Common.Authentication.Oidc;

public class ClientCredentialsAccessTokenAcquirer(
    HttpClient httpClient,
    IOptions<ClientCredentialsAccessTokenAcquirerSettings> settings,
    TimeProvider timeProvider) : IAccessTokenAcquirer
{
    public async Task<AccessToken> AcquireAccessToken(CancellationToken cancellationToken)
    {
        var settingsValue = settings.Value;
        var requestTime = timeProvider.GetUtcNow();
        var response = await httpClient.RequestTokenAsync(
            new TokenRequest
            {
                Address = "/connect/token",
                ClientId = settingsValue.ClientId,
                ClientSecret = settingsValue.ClientSecret,
                GrantType = OidcConstants.GrantTypes.ClientCredentials,
                Parameters = new Parameters
                {
                    { OidcConstants.TokenRequest.Scope, settingsValue.Scope }
                }
            },
            cancellationToken);

        if (response.IsError)
        {
            throw new AuthenticationException($"Authentication failed: {response.ErrorDescription}");
        }

        if (response.AccessToken == null)
        {
            throw new AuthenticationException($"Authentication failed: access token is null.");
        }

        return new AccessToken(response.AccessToken, requestTime.AddSeconds(response.ExpiresIn));
    }
}

public class ClientCredentialsAccessTokenAcquirer<T>(
    HttpClient httpClient,
    IOptions<ClientCredentialsAccessTokenAcquirerSettings<T>> settings,
    TimeProvider timeProvider) :
    ClientCredentialsAccessTokenAcquirer(httpClient, settings, timeProvider), IAccessTokenAcquirer<T>;
