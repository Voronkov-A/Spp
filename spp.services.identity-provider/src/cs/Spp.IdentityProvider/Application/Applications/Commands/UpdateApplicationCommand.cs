using Spp.Common.Mediator;
using Spp.IdentityProvider.WebApi.Applications.V1;

namespace Spp.IdentityProvider.Application.Applications.Commands;

public readonly struct UpdateApplicationCommand(
    Domain.Applications.ApplicationId applicationId,
    UpdateApplicationRequest data) :
    IRequest<UpdateApplicationCommandResponse>
{
    public Domain.Applications.ApplicationId ApplicationId { get; } = applicationId;

    public UpdateApplicationRequest Data { get; } = data;
}
