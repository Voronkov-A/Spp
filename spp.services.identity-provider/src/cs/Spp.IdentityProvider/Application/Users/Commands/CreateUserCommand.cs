using Spp.Common.Mediator;
using Spp.IdentityProvider.WebApi.Users.V1;

namespace Spp.IdentityProvider.Application.Users.Commands;

public readonly struct CreateUserCommand(CreateUserRequest data) : IRequest<CreateUserCommandResponse>
{
    public CreateUserRequest Data { get; } = data;
}
