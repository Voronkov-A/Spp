using Spp.Common.Cqs;
using Spp.Common.Domain;

namespace Spp.Authorization.Application.Users.Commands;

public readonly struct AssignUserRoleCommand(EntityId userId, EntityId roleId) :
    ICommand<AssignUserRoleCommandResponse>, IRequiresUnitOfWork
{
    public EntityId UserId { get; } = userId;

    public EntityId RoleId { get; } = roleId;
}
