#nullable enable

namespace Spp.Authorization.WebApi.Rbac.V1;


/// <summary>
/// Create role request.
/// </summary>
public partial class CreateRoleRequest
{
    public CreateRoleRequest(
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
    /// Role permission references.
    /// </summary>
    public System.Collections.Generic.IReadOnlyList<PermissionReference> Permissions { get; }
}


#nullable restore
