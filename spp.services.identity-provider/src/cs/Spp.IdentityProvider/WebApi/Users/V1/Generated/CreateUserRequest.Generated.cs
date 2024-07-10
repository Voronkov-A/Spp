#nullable enable

namespace Spp.IdentityProvider.WebApi.Users.V1;


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
    public string Username { get; }
    /// <summary>
    /// Password.
    /// </summary>
    public string Password { get; }
}


#nullable restore
