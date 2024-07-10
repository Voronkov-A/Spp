#nullable enable

namespace Spp.IdentityProvider.Client.Auth.V1;


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
    [System.Text.Json.Serialization.JsonPropertyName("accessToken")]
    public string AccessToken { get; }
    /// <summary>
    /// Refresh token.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("refreshToken")]
    public string? RefreshToken { get; }
}


#nullable restore
