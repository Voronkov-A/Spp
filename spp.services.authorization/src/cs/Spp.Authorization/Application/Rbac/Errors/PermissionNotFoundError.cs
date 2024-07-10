using Spp.Common.Domain;

namespace Spp.Authorization.Application.Rbac.Errors;

public readonly struct PermissionNotFoundError(EntityId permissionGroupId, EntityId permissionId)
{
    public EntityId PermissionGroupId { get; } = permissionGroupId;

    public EntityId PermissionId { get; } = permissionId;
}
