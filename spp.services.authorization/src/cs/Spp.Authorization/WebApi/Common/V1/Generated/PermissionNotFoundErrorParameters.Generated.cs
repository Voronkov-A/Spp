#nullable enable

namespace Spp.Authorization.WebApi.Common.V1;


/// <summary>
/// Parameters for iauthorization.permissionNotFound error code.
/// </summary>
public partial class PermissionNotFoundErrorParameters
{
    public PermissionNotFoundErrorParameters(
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
