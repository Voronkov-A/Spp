using Spp.IdentityProvider.Application.Applications.Errors;
using Spp.IdentityProvider.WebApi.Applications.V1;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spp.Common.Mediator;
using Spp.IdentityProvider.Persistence.Authorization;

namespace Spp.IdentityProvider.Application.Applications.Commands;

public class CreateApplicationCommandHandler(AuthorizationDbContext dbContext) :
    IRequestHandler<CreateApplicationCommand, CreateApplicationCommandResponse>
{
    public async Task<CreateApplicationCommandResponse> Handle(
        CreateApplicationCommand request,
        CancellationToken cancellationToken)
    {
        var data = request.Data;

        if (await dbContext.Applications.AnyAsync(x => x.ClientId == data.ClientId, cancellationToken))
        {
            return new(new DuplicateClientIdError(data.ClientId));
        }

        var id = new Domain.Applications.ApplicationId();
        var application = new Domain.Applications.Application(
            id: id,
            clientId: request.Data.ClientId,
            clientSecret: request.Data.ClientSecret,
            redirectUris: request.Data.RedirectUris);
        await dbContext.Applications.AddAsync(application, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new(new CreateApplicationResponse(id.ToString()));
    }
}
