using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Postgres;

public interface IDatabase
{
    DbConnection CreateConnection();

    Task EnsureExists(CancellationToken cancellationToken);

    Task DropIfExists(CancellationToken cancellationToken);
}
