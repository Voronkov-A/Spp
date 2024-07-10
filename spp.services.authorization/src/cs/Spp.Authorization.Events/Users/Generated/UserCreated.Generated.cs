#nullable enable

namespace Spp.Authorization.Events.Users;


/// <summary>
/// User created.
/// </summary>
public partial class UserCreated
{
    public UserCreated(
        string name,
        bool isSuperuser,
        System.Collections.Generic.IReadOnlyList<UserIdentity> identities,
        System.Collections.Generic.IReadOnlyList<string> roleIds
    )
    {
        this.Name = name;
        this.IsSuperuser = isSuperuser;
        this.Identities = identities;
        this.RoleIds = roleIds;
    }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// Is superuser.
    /// </summary>
    public bool IsSuperuser { get; }
    /// <summary>
    /// Identities.
    /// </summary>
    public System.Collections.Generic.IReadOnlyList<UserIdentity> Identities { get; }
    /// <summary>
    /// Role identifiers.
    /// </summary>
    public System.Collections.Generic.IReadOnlyList<string> RoleIds { get; }
}


#nullable restore
