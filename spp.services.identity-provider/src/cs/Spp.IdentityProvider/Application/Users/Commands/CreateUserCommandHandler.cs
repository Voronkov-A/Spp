using Microsoft.AspNetCore.Identity;
using Spp.IdentityProvider.Domain.Users;
using System;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Mediator;
using Spp.IdentityProvider.WebApi.Users.V1;
using Spp.IdentityProvider.Application.Users.Errors;

namespace Spp.IdentityProvider.Application.Users.Commands;

public class CreateUserCommandHandler(UserManager<User> userManager)
    : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
{
    public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var data = request.Data;

        if (await userManager.FindByNameAsync(data.Username) != null)
        {
            return new(new DuplicateUsernameError());
        }

        var id = new UserId();
        var user = new User(id, data.Username);
        var result = await userManager.CreateAsync(user, data.Password);

        if (!result.Succeeded)
        {
            throw new ApplicationException($"Error while creating user '{id}': {result}.");
        }

        return new(new CreateUserResponse(id.ToString()));
    }
}
