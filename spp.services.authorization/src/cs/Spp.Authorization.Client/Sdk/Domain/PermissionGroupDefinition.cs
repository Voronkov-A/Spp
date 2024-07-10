using System.Collections.Generic;
using System.Linq;
using Spp.Common.Domain;

namespace Spp.Authorization.Client.Sdk.Domain;

public class PermissionGroupDefinition(EntityId id, IEnumerable<EntityId> permissions)
{
    public EntityId Id { get; } = id;

    public IReadOnlyCollection<EntityId> Permissions { get; } = permissions.ToList();
}
