#nullable enable

namespace Spp.Authorization.WebApi.Rbac.V1;


/// <summary>
/// Create or update permission group request.
/// </summary>
public partial class CreateOrUpdatePermissionGroupRequest
{
    public CreateOrUpdatePermissionGroupRequest(
        System.Collections.Generic.IReadOnlyList<Permission> permissions
    )
    {
        this.Permissions = permissions;
    }

    /// <summary>
    /// Permissions.
    /// </summary>
    public System.Collections.Generic.IReadOnlyList<Permission> Permissions { get; }
}


#nullable restore
