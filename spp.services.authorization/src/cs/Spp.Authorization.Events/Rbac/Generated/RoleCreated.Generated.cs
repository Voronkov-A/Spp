#nullable enable

namespace Spp.Authorization.Events.Rbac;


/// <summary>
/// Role created.
/// </summary>
public partial class RoleCreated
{
    public RoleCreated(
        LocalizedName name,
        bool isDefault,
        System.Collections.Generic.IReadOnlyList<PermissionReference> permissions
    )
    {
        this.Name = name;
        this.IsDefault = isDefault;
        this.Permissions = permissions;
    }

    /// <summary>
    /// 
    /// </summary>
    public LocalizedName Name { get; }
    /// <summary>
    /// If true, the role will be assigned to all newly registered users.
    /// </summary>
    public bool IsDefault { get; }
    /// <summary>
    /// Role permissions.
    /// </summary>
    public System.Collections.Generic.IReadOnlyList<PermissionReference> Permissions { get; }
}


#nullable restore
