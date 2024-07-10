using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Spp.Common.Miscellaneous;
using Spp.IdentityProvider.Application.Applications.Settings;
using Spp.IdentityProvider.Persistence.Authorization;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Mediator;

namespace Spp.IdentityProvider.Application.Applications.Commands;

public class CreateOrUpdateDefaultApplicationCommandHandler(
    AuthorizationDbContext dbContext,
    IOptions<DefaultApplicationSettings> settings)
    : IRequestHandler<CreateOrUpdateDefaultApplicationCommand, Unit>
{
    public async Task<Unit> Handle(CreateOrUpdateDefaultApplicationCommand request, CancellationToken cancellationToken)
    {
        var data = settings.Value;

        var application = await dbContext.Applications
            .SingleOrDefaultAsync(x => x.ClientId == data.ClientId, cancellationToken);

        if (application == null)
        {
            application = new Domain.Applications.Application(
                id: new Domain.Applications.ApplicationId(),
                clientId: data.ClientId,
                clientSecret: data.ClientSecret,
                redirectUris: data.RedirectUris);
            await dbContext.AddAsync(application, cancellationToken);
        }
        else
        {
            application.Update(data.ClientSecret, data.RedirectUris);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return default;
    }
}
