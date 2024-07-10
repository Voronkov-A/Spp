using Spp.Common.Domain;

namespace Spp.Authorization.Domain.Rbac;

public readonly struct Permission(EntityId id) : IEntity
{
    public EntityId Id { get; } = id;
}
