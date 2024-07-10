#nullable enable

namespace Spp.IdentityProvider.WebApi.Users.V1;


/// <summary>
/// Create user response.
/// </summary>
public partial class CreateUserResponse
{
    public CreateUserResponse(
        string id
    )
    {
        this.Id = id;
    }

    /// <summary>
    /// User identifier.
    /// </summary>
    public string Id { get; }
}


#nullable restore
