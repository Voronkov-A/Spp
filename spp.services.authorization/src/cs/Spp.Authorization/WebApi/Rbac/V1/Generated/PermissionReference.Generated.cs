#nullable enable

namespace Spp.Authorization.WebApi.Rbac.V1;


/// <summary>
/// Permission reference.
/// </summary>
public partial class PermissionReference
{
    public PermissionReference(
        string permissionGroupId,
        string permissionId
    )
    {
        this.PermissionGroupId = permissionGroupId;
        this.PermissionId = permissionId;
    }

    /// <summary>
    /// Permission group identifier.
    /// </summary>
    public string PermissionGroupId { get; }
    /// <summary>
    /// Permission identifier.
    /// </summary>
    public string PermissionId { get; }
}


#nullable restore
