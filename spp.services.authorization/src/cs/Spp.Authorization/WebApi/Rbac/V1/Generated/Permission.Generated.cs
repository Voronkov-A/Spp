#nullable enable

namespace Spp.Authorization.WebApi.Rbac.V1;


/// <summary>
/// Permission.
/// </summary>
public partial class Permission
{
    public Permission(
        string id
    )
    {
        this.Id = id;
    }

    /// <summary>
    /// Identifier.
    /// </summary>
    public string Id { get; }
}


#nullable restore
