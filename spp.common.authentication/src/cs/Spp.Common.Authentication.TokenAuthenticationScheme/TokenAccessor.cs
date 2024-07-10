using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace Spp.Common.Authentication.TokenAuthenticationScheme;

public class TokenAccessor(IOptions<TokenAccessorOptions> options) : ITokenAccessor
{
    public void DeleteTokens(HttpResponse response)
    {
        response.Cookies.Delete(options.Value.AccessTokenCookieName);
        response.Cookies.Delete(options.Value.RefreshTokenCookieName);
    }

    public string? GetAccessToken(HttpRequest request)
    {
        const string bearerPrefix = "Bearer ";
        var authorizationHeader = request.Headers.Authorization
            .FirstOrDefault(x => x != null && x.StartsWith(bearerPrefix));
        return authorizationHeader == null
            ? request.Cookies[options.Value.AccessTokenCookieName]
            : authorizationHeader[bearerPrefix.Length..];
    }

    public void SetAccessToken(HttpResponse response, string token)
    {
        response.OnStarting(() =>
        {
            response.Cookies.Append(options.Value.AccessTokenCookieName, token, new CookieOptions
            {
                HttpOnly = true
            });
            return Task.CompletedTask;
        });
    }

    public void SetRefreshToken(HttpResponse response, string token)
    {
        response.OnStarting(() =>
        {
            response.Cookies.Append(options.Value.RefreshTokenCookieName, token, new CookieOptions
            {
                HttpOnly = true
            });
            return Task.CompletedTask;
        });
    }
}
