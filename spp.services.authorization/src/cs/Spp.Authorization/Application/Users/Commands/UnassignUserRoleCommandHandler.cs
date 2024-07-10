using Spp.Authorization.Application.Users.Errors;
using Spp.Authorization.Domain.Rbac.Repositories;
using Spp.Authorization.Domain.Users.Repositories;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Application.Users.Commands;

public class UnassignUserRoleCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository) :
    IRequestHandler<UnassignUserRoleCommand, UnassignUserRoleCommandResponse>
{
    public async Task<UnassignUserRoleCommandResponse> Handle(
        UnassignUserRoleCommand request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.Find(request.UserId, cancellationToken);

        if (user == null)
        {
            return new(new UserNotFoundError(request.UserId));
        }

        var role = await roleRepository.Find(request.RoleId, cancellationToken);

        if (role == null || !user.Roles.Contains(role.Id))
        {
            return new(new RoleNotFoundError(request.RoleId));
        }

        user.Unassign(role);
        return new(new Unit());
    }
}
