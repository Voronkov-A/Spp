#nullable enable

namespace Spp.IdentityProvider.Client.Connect.V1;


/// <summary>
/// Token data.
/// </summary>
public partial class TokenData
{
    public TokenData(
        string accessToken,
        string? tokenType,
        long? expiresIn,
        string? refreshToken,
        string? scope,
        string? idToken,
        string? idTokenType
    )
    {
        this.AccessToken = accessToken;
        this.TokenType = tokenType;
        this.ExpiresIn = expiresIn;
        this.RefreshToken = refreshToken;
        this.Scope = scope;
        this.IdToken = idToken;
        this.IdTokenType = idTokenType;
    }

    /// <summary>
    /// The Access Token for the given token request.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("access_token")]
    public string AccessToken { get; }
    /// <summary>
    /// The Token Type issued.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("token_type")]
    public string? TokenType { get; }
    /// <summary>
    /// The expiry time, in seconds.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("expires_in")]
    public long? ExpiresIn { get; }
    /// <summary>
    /// The refresh token, if applicable.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; }
    /// <summary>
    /// The issued scope.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("scope")]
    public string? Scope { get; }
    /// <summary>
    /// If the requested SCOPE included &#39;msso&#39; or &#39;openid&#39;, response includes an id_token. 
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("id_token")]
    public string? IdToken { get; }
    /// <summary>
    /// If the requested SCOPE included &#39;msso&#39; or &#39;openid&#39;, response includes an id_token_type. 
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("id_token_type")]
    public string? IdTokenType { get; }
}


#nullable restore
