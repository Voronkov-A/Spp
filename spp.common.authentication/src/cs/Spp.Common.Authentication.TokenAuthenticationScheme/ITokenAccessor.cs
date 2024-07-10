using Microsoft.AspNetCore.Http;

namespace Spp.Common.Authentication.TokenAuthenticationScheme;

public interface ITokenAccessor
{
    string? GetAccessToken(HttpRequest request);

    void SetAccessToken(HttpResponse response, string token);

    void SetRefreshToken(HttpResponse response, string token);

    void DeleteTokens(HttpResponse response);
}
