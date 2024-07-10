using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Migrations;

public interface IMigrationStore
{
    Task Initialize(CancellationToken cancellationToken);

    Task<int?> FindLastMigrationIndex(CancellationToken cancellationToken);

    Task AddMigration(IMigration migration, CancellationToken cancellationToken);
}

public interface IMigrationStore<T> : IMigrationStore;
