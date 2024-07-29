using Microsoft.EntityFrameworkCore;
using Spp.IdentityProvider.Persistence.Authorization;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Initialization;

namespace Spp.IdentityProvider.Initialization;

public class PersistenceInitializer(AuthorizationDbContext authorizationDbContext) : IInitializer
{
    private readonly AuthorizationDbContext _authorizationDbContext = authorizationDbContext;

    public async Task Initialize(CancellationToken cancellationToken)
    {
        await _authorizationDbContext.Database.MigrateAsync(cancellationToken);
    }
}
