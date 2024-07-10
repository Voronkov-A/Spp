#nullable enable

namespace Spp.IdentityProvider.Client.Auth.V1;


/// <summary>
/// Sign in request.
/// </summary>
public partial class SignInRequest
{
    public SignInRequest(
        string username,
        string password,
        string scopes
    )
    {
        this.Username = username;
        this.Password = password;
        this.Scopes = scopes;
    }

    /// <summary>
    /// Username.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("username")]
    public string Username { get; }
    /// <summary>
    /// Password.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("password")]
    public string Password { get; }
    /// <summary>
    /// Scopes.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("scopes")]
    public string Scopes { get; }
}


#nullable restore
