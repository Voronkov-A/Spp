#nullable enable

namespace Spp.Authorization.WebApi.Rbac.V1;


/// <summary>
/// Create role response.
/// </summary>
public partial class CreateRoleResponse
{
    public CreateRoleResponse(
        string id
    )
    {
        this.Id = id;
    }

    /// <summary>
    /// Role identifier.
    /// </summary>
    public string Id { get; }
}


#nullable restore
