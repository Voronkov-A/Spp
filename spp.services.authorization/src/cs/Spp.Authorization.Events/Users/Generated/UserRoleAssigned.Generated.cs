#nullable enable

namespace Spp.Authorization.Events.Users;


/// <summary>
/// User role assigned.
/// </summary>
public partial class UserRoleAssigned
{
    public UserRoleAssigned(
        string roleId
    )
    {
        this.RoleId = roleId;
    }

    /// <summary>
    /// Role identifier.
    /// </summary>
    public string RoleId { get; }
}


#nullable restore
