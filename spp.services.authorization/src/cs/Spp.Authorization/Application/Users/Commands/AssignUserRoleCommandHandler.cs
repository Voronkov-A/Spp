using Spp.Authorization.Application.Users.Errors;
using Spp.Authorization.Domain.Rbac.Repositories;
using Spp.Authorization.Domain.Users.Repositories;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Application.Users.Commands;

public class AssignUserRoleCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository) :
    IRequestHandler<AssignUserRoleCommand, AssignUserRoleCommandResponse>
{
    public async Task<AssignUserRoleCommandResponse> Handle(
        AssignUserRoleCommand request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.Find(request.UserId, cancellationToken);

        if (user == null)
        {
            return new(new UserNotFoundError(request.UserId));
        }

        var role = await roleRepository.Find(request.RoleId, cancellationToken);

        if (role == null)
        {
            return new(new RoleNotFoundError(request.RoleId));
        }

        user.Assign(role);
        return new(new Unit());
    }
}
