#nullable enable

namespace Spp.IdentityProvider.WebApi.Auth.V1;


/// <summary>
/// Token data.
/// </summary>
public partial class TokenData
{
    public TokenData(
        string accessToken,
        string? refreshToken
    )
    {
        this.AccessToken = accessToken;
        this.RefreshToken = refreshToken;
    }

    /// <summary>
    /// Access token.
    /// </summary>
    public string AccessToken { get; }
    /// <summary>
    /// Refresh token.
    /// </summary>
    public string? RefreshToken { get; }
}


#nullable restore
