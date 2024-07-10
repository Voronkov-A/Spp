#nullable enable

namespace Spp.Authorization.Events.Rbac;


/// <summary>
/// Permission group updated.
/// </summary>
public partial class PermissionGroupUpdated
{
    public PermissionGroupUpdated(
        System.Collections.Generic.IReadOnlyList<string> addedPermissionIds,
        System.Collections.Generic.IReadOnlyList<string> removedPermissionIds
    )
    {
        this.AddedPermissionIds = addedPermissionIds;
        this.RemovedPermissionIds = removedPermissionIds;
    }

    /// <summary>
    /// Added permission ids.
    /// </summary>
    public System.Collections.Generic.IReadOnlyList<string> AddedPermissionIds { get; }
    /// <summary>
    /// Removed permission ids.
    /// </summary>
    public System.Collections.Generic.IReadOnlyList<string> RemovedPermissionIds { get; }
}


#nullable restore
