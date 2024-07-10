#nullable enable

namespace Spp.Authorization.Events.Users;


/// <summary>
/// User role unassigned.
/// </summary>
public partial class UserRoleUnassigned
{
    public UserRoleUnassigned(
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
