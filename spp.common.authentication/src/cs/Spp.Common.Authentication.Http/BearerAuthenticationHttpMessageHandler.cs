using Spp.Common.Authentication.Abstractions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Authentication.Http;

public class BearerAuthenticationHttpMessageHandler(
    IAuthenticationContext authenticationContext,
    IAccessTokenAcquirer accessTokenAcquirer,
    AccessTokenCache accessTokenCache) :
    DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (!authenticationContext.IsActive)
        {
            return await base.SendAsync(request, cancellationToken);
        }

        await SetAuthorizationHeader(request, useCache: true, cancellationToken);
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode != HttpStatusCode.Unauthorized)
        {
            return response;
        }

        response.Dispose();
        await SetAuthorizationHeader(request, useCache: false, cancellationToken);
        return await base.SendAsync(request, cancellationToken);
    }

    private async Task SetAuthorizationHeader(
        HttpRequestMessage request,
        bool useCache,
        CancellationToken cancellationToken)
    {
        if (!useCache || !accessTokenCache.TryGet(GetType(), out var token))
        {
            token = await accessTokenAcquirer.AcquireAccessToken(cancellationToken);
            accessTokenCache.Set(GetType(), token);
        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
    }
}

public class BearerAuthenticationHttpMessageHandler<T>(
    IAuthenticationContext<T> authenticationContext,
    IAccessTokenAcquirer<T> accessTokenAcquirer,
    AccessTokenCache accessTokenCache) :
    BearerAuthenticationHttpMessageHandler(authenticationContext, accessTokenAcquirer, accessTokenCache);
