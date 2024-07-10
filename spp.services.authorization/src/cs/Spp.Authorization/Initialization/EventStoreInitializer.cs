using Spp.Authorization.Persistence.Schemas.Indices;
using Spp.Common.EventSourcing.EventStore.Postgres;
using Spp.Common.Initialization;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Initialization;

public class EventStoreInitializer(IEventStoreMigrator eventStoreMigrator, IIndicesMigrator indicesMigrator) :
    IInitializer
{
    public async Task Initialize(CancellationToken cancellationToken)
    {
        await eventStoreMigrator.Migrate(cancellationToken);
        await indicesMigrator.Up(cancellationToken);
    }
}
