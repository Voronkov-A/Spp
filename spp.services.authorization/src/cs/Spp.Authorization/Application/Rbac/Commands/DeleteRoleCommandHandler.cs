using Spp.Authorization.Application.Rbac.Errors;
using Spp.Authorization.Domain.Rbac.Repositories;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Application.Rbac.Commands;

public class DeleteRoleCommandHandler(IRoleRepository repository) :
    IRequestHandler<DeleteRoleCommand, DeleteRoleCommandResponse>
{
    public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await repository.Find(request.RoleId, cancellationToken);

        if (role == null)
        {
            return new(new RoleNotFoundError(request.RoleId));
        }

        role.Delete();
        return new(new Unit());
    }
}
