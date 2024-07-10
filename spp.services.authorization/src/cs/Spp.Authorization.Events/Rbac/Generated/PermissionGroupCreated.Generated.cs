#nullable enable

namespace Spp.Authorization.Events.Rbac;


/// <summary>
/// Permission group created.
/// </summary>
public partial class PermissionGroupCreated
{
    public PermissionGroupCreated(
        System.Collections.Generic.IReadOnlyList<string> permissionIds
    )
    {
        this.PermissionIds = permissionIds;
    }

    /// <summary>
    /// Permission ids.
    /// </summary>
    public System.Collections.Generic.IReadOnlyList<string> PermissionIds { get; }
}


#nullable restore
