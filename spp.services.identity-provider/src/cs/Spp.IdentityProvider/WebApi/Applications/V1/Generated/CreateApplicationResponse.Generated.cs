#nullable enable

namespace Spp.IdentityProvider.WebApi.Applications.V1;


/// <summary>
/// Create application response.
/// </summary>
public partial class CreateApplicationResponse
{
    public CreateApplicationResponse(
        string id
    )
    {
        this.Id = id;
    }

    /// <summary>
    /// Application identifier.
    /// </summary>
    public string Id { get; }
}


#nullable restore
