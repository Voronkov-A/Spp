using System.Collections.Generic;
using System.Linq;
using Spp.Common.Domain;

namespace Spp.Authorization.Client.Sdk.Domain;

internal class Role(EntityId id, IEnumerable<PermissionReference> permissions)
{
    private readonly HashSet<PermissionReference> _permissions = permissions.ToHashSet();

    public EntityId Id { get; } = id;

    public bool HasPermission(PermissionReference permission)
    {
        return _permissions.Contains(permission);
    }
}
