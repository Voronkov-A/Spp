namespace Spp.Common.Authentication.TokenAuthenticationScheme;

public class TokenAccessorOptions
{
    public string AccessTokenCookieName { get; set; } = "A";

    public string RefreshTokenCookieName { get; set; } = "R";
}
