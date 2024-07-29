using Microsoft.Extensions.Options;
using Spp.Common.Postgres;

namespace Spp.Common.Synchronization.Postgres;

public class SynchronizationDatabase(
    IConnectionFactory connectionFactory,
    IOptions<PostgresTableDistributedLockSettings> settings,
    IOptions<PostgresTableDistributedLockOptions> options) :
    PostgresDatabase(connectionFactory, settings.Value)
{
    public string SchemaName => options.Value.SchemaName;
}
