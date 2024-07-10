using Spp.Authorization.WebApi.Rbac.V1;
using Spp.Common.Cqs;

namespace Spp.Authorization.Application.Rbac.Commands;

public readonly struct CreateRoleCommand(CreateRoleRequest data) :
    ICommand<CreateRoleCommandResponse>,
    IRequiresUnitOfWork
{
    public CreateRoleRequest Data { get; } = data;
}
