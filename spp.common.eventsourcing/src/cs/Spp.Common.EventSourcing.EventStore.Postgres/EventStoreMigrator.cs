using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.EventSourcing.EventStore.Postgres;

public class EventStoreMigrator(EventStoreDatabase database) : IEventStoreMigrator
{
    private readonly string _createSchemaCommandText = $"""
        create schema if not exists {database.SchemaName ?? "public"};

        create table if not exists {database.SchemaName ?? "public"}.events (
            aggregate_id text not null,
            aggregate_type text not null,
            version bigint not null,
            type text not null,
            data bytea not null,
            constraint pk_events primary key (aggregate_id, aggregate_type, version)
        );
        """;

    public async Task Migrate(CancellationToken cancellationToken)
    {
        await database.EnsureExists(cancellationToken);
        await using var connection = database.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = _createSchemaCommandText;
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
