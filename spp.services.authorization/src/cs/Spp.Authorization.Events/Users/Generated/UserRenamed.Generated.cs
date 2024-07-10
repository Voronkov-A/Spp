#nullable enable

namespace Spp.Authorization.Events.Users;


/// <summary>
/// User renamed.
/// </summary>
public partial class UserRenamed
{
    public UserRenamed(
        string name
    )
    {
        this.Name = name;
    }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; }
}


#nullable restore
