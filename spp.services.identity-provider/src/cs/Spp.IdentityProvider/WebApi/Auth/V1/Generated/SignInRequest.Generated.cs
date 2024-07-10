#nullable enable

namespace Spp.IdentityProvider.WebApi.Auth.V1;


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
    public string Username { get; }
    /// <summary>
    /// Password.
    /// </summary>
    public string Password { get; }
    /// <summary>
    /// Scopes.
    /// </summary>
    public string Scopes { get; }
}


#nullable restore
