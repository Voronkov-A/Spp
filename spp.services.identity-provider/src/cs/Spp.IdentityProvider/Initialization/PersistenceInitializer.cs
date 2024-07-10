using Microsoft.EntityFrameworkCore;
using Spp.IdentityProvider.Persistence.Authorization;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Initialization;

namespace Spp.IdentityProvider.Initialization;

public class PersistenceInitializer(AuthorizationDbContext authorizationDbContext) : IInitializer
{
    public async Task Initialize(CancellationToken cancellationToken)
    {
        await authorizationDbContext.Database.MigrateAsync(cancellationToken);
    }
}
