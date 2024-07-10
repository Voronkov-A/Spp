using Microsoft.Extensions.Options;
using Spp.Common.Postgres;

namespace Spp.Common.EventSourcing.EventStore.Postgres;

public class EventStoreDatabase(
    IConnectionFactory connectionFactory,
    IOptions<PostgresEventStoreConnectionSettings> settings,
    IOptions<PostgresEventStoreSchemaOptions> options) :
    PostgresDatabase(connectionFactory, settings.Value)
{
    public string SchemaName => options.Value.SchemaName;
}
