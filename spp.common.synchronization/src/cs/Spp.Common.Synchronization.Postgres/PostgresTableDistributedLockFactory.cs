using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Synchronization.Postgres;

internal class PostgresTableDistributedLockFactory(SynchronizationDatabase database) : IDistributedLockFactory
{
    private readonly SynchronizationDatabase _database = database;

    public Task<IDistributedLock> Create(string id, CancellationToken cancellationToken)
    {
        IDistributedLock result = new PostgresTableDistributedLock(id, _database);
        return Task.FromResult(result);
    }
}
