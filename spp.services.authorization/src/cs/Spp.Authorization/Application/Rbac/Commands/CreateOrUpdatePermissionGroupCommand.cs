using Spp.Authorization.WebApi.Rbac.V1;
using Spp.Common.Cqs;
using Spp.Common.Domain;

namespace Spp.Authorization.Application.Rbac.Commands;

public readonly struct CreateOrUpdatePermissionGroupCommand(
    EntityId permissionGroupId,
    CreateOrUpdatePermissionGroupRequest data) :
    ICommand, IRequiresUnitOfWork
{
    public EntityId PermissionGroupId { get; } = permissionGroupId;

    public CreateOrUpdatePermissionGroupRequest Data { get; } = data;
}
