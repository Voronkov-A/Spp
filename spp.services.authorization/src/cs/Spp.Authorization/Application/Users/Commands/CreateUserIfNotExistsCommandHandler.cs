using Microsoft.AspNetCore.Identity;
using Spp.Authorization.Application.Users.Models;
using Spp.Authorization.Domain.Users;
using Spp.Authorization.Domain.Users.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Spp.Authorization.Domain.Rbac.Repositories;
using Spp.Common.Domain;
using Spp.Common.Mediator;

namespace Spp.Authorization.Application.Users.Commands;

public class CreateUserIfNotExistsCommandHandler(
    IUserRepository userRepository,
    IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
    IUserNameGenerator userNameGenerator,
    IRoleRepository roleRepository) :
    IRequestHandler<CreateUserIfNotExistsCommand, CreateUserIfNotExistsResponse>
{
    public async Task<CreateUserIfNotExistsResponse> Handle(
        CreateUserIfNotExistsCommand request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.Find(request.Identity, cancellationToken);

        if (user == null)
        {
            var name = await userNameGenerator.GenerateName(cancellationToken);
            var roles = await roleRepository.GetAll(isDefault: true, cancellationToken);
            user = new User(
                id: new EntityId(),
                name: name,
                isSuperuser: false,
                identity: request.Identity,
                roles: roles);
            await userRepository.Add(user, cancellationToken);
        }

        var principal = await userClaimsPrincipalFactory.CreateAsync(user);
        return new CreateUserIfNotExistsResponse(principal, isBlocked: user.IsBlocked);
    }
}
