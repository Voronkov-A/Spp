using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spp.Common.Mediator;
using Spp.IdentityProvider.Persistence.Authorization;
using Spp.IdentityProvider.WebApi.Applications.V1;

namespace Spp.IdentityProvider.Application.Applications.Queries;

public class ListApplicationsQueryHandler(AuthorizationDbContext dbContext) :
    IRequestHandler<ListApplicationsQuery, ApplicationViewList>
{
    public async Task<ApplicationViewList> Handle(ListApplicationsQuery request, CancellationToken cancellationToken)
    {
        var applications = await dbContext.Applications
            .Where(x => x.ClientId == request.ClientId)
            .ToListAsync(cancellationToken);
        var applicationViews = applications
            .Select(x => new ApplicationView(
                id: x.Id.ToString(),
                clientId: x.ClientId,
                redirectUris: x.RedirectUris.ToList()))
            .ToList();
        return new ApplicationViewList(applicationViews);
    }
}
