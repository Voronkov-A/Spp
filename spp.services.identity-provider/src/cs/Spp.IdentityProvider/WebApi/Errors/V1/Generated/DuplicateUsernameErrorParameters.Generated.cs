#nullable enable

namespace Spp.IdentityProvider.WebApi.Errors.V1;


/// <summary>
/// Parameters for identityProvider.duplicateUsername error code.
/// </summary>
public partial class DuplicateUsernameErrorParameters
{
    public DuplicateUsernameErrorParameters(
        string username
    )
    {
        this.Username = username;
    }

    /// <summary>
    /// Username.
    /// </summary>
    public string Username { get; }
}


#nullable restore
