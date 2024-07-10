using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Spp.Common.Hosting.Controllers;
using Spp.Common.Mediator;
using Spp.IdentityProvider.Application.Users.Commands;
using Spp.IdentityProvider.Application.Users.Errors;
using Spp.IdentityProvider.WebApi.Errors;
using Spp.IdentityProvider.WebApi.Errors.V1;

namespace Spp.IdentityProvider.WebApi.Users.V1;

[Authorize]
public class UsersController(IIdentityProviderErrorFactory errorFactory, IMediator mediator) :
    BaseUsersController
{
    protected override async Task<CreateActionResult> Create(
        CreateUserRequest createUserRequest,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Dispatch<CreateUserCommand, CreateUserCommandResponse>(
            new CreateUserCommand(createUserRequest),
            cancellationToken);

        return response.Switch(
            (CreateUserResponse x) =>
            {
                this.AddCreatedResourceLocation(x.Id);
                return CreateActionResult.Create201(x);
            },
            (DuplicateUsernameError x) => CreateActionResult.Create400(errorFactory.DuplicateUsername(
                $"User with name '{x.Username}' already exists.",
                new DuplicateUsernameErrorParameters(x.Username))));
    }
}
