using Spp.Authorization.Client.Sdk.Persistence.Schema;
using Spp.Common.Initialization;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Client.Sdk.Initialization;

internal class AuthorizationPersistenceInitializer(IAuthorizationMigrator migrator) : IInitializer
{
    public async Task Initialize(CancellationToken cancellationToken)
    {
        await migrator.Up(cancellationToken);
    }
}
