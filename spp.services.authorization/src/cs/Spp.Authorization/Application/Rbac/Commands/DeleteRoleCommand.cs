using Spp.Common.Cqs;
using Spp.Common.Domain;

namespace Spp.Authorization.Application.Rbac.Commands;

public readonly struct DeleteRoleCommand(EntityId roleId) : ICommand<DeleteRoleCommandResponse>, IRequiresUnitOfWork
{
    public EntityId RoleId { get; } = roleId;
}
