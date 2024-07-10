using Spp.IdentityProvider.Application.Applications.Commands;
using Spp.IdentityProvider.Application.Applications.Errors;
using Spp.IdentityProvider.WebApi.Errors.V1;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Errors;
using Spp.Common.Hosting.Controllers;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;
using Spp.IdentityProvider.Application.Applications.Queries;
using Spp.IdentityProvider.WebApi.Errors;

namespace Spp.IdentityProvider.WebApi.Applications.V1;

public class ApplicationsController(
    IIdentityProviderErrorFactory errorFactory,
    ICommonErrorFactory commonErrorFactory,
    IMediator mediator) :
    BaseApplicationsController
{
    protected override async Task<CreateActionResult> Create(
        CreateApplicationRequest createApplicationRequest,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Dispatch<CreateApplicationCommand, CreateApplicationCommandResponse>(
            new CreateApplicationCommand(createApplicationRequest),
            cancellationToken);
        return result.Switch(
            (CreateApplicationResponse x) =>
            {
                this.AddCreatedResourceLocation(x.Id);
                return CreateActionResult.Create201(x);
            },
            (DuplicateClientIdError x) => CreateActionResult.Create400(errorFactory.DuplicateClientId(
                $"Application with client id '{x.ClientId}' already exists.",
                new DuplicateClientIdErrorParameters(x.ClientId))));
    }

    protected override async Task<ListActionResult> List(string clientId, CancellationToken cancellationToken)
    {
        var result = await mediator.Dispatch<ListApplicationsQuery, ApplicationViewList>(
            new ListApplicationsQuery(clientId),
            cancellationToken);
        return ListActionResult.Create200(result);
    }

    protected override async Task<UpdateActionResult> Update(
        string id,
        UpdateApplicationRequest updateApplicationRequest,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Dispatch<UpdateApplicationCommand, UpdateApplicationCommandResponse>(
            new UpdateApplicationCommand(new Domain.Applications.ApplicationId(id), updateApplicationRequest),
            cancellationToken);
        return result.Switch(
            (Unit _) => UpdateActionResult.Create204(),
            (ApplicationNotFoundError x) => UpdateActionResult.Create404(commonErrorFactory.ResourceNotFound(
                $"Application with id '{id}' does not exists.")));
    }
}
