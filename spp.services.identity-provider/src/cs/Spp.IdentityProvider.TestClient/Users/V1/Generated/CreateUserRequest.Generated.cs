#nullable enable

namespace Spp.IdentityProvider.TestClient.Users.V1;


/// <summary>
/// Create user request.
/// </summary>
public partial class CreateUserRequest
{
    public CreateUserRequest(
        string username,
        string password
    )
    {
        this.Username = username;
        this.Password = password;
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
}


#nullable restore
