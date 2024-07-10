using Spp.Common.Mediator;
using Spp.IdentityProvider.WebApi.Applications.V1;

namespace Spp.IdentityProvider.Application.Applications.Commands;

public readonly struct CreateApplicationCommand(CreateApplicationRequest data) :
    IRequest<CreateApplicationCommandResponse>
{
    public CreateApplicationRequest Data { get; } = data;
}
