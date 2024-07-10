using Microsoft.EntityFrameworkCore;
using Spp.Common.Miscellaneous;
using Spp.IdentityProvider.Application.Applications.Errors;
using Spp.IdentityProvider.Persistence.Authorization;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Mediator;

namespace Spp.IdentityProvider.Application.Applications.Commands;

public class UpdateApplicationCommandHandler(AuthorizationDbContext dbContext)
    : IRequestHandler<UpdateApplicationCommand, UpdateApplicationCommandResponse>
{
    public async Task<UpdateApplicationCommandResponse> Handle(
        UpdateApplicationCommand request,
        CancellationToken cancellationToken)
    {
        var application = await dbContext.Applications
            .SingleOrDefaultAsync(x => x.Id == request.ApplicationId, cancellationToken);

        if (application == null)
        {
            return new(new ApplicationNotFoundError());
        }

        application.Update(request.Data.ClientSecret, request.Data.RedirectUris);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new(new Unit());
    }
}
