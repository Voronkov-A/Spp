namespace Spp.Common.Authentication.TokenAuthenticationScheme;

public class TokenGenerationResult(string accessToken, string? refreshToken)
{
    public string AccessToken { get; } = accessToken;

    public string? RefreshToken { get; } = refreshToken;
}
