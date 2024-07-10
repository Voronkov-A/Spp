using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.EventSourcing.EventStore.Postgres;

public interface IEventStoreMigrator
{
    Task Migrate(CancellationToken cancellationToken);
}
