using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.EventSourcing.EventStore.Postgres;

public class PostgresEventStore(IEventSerializer serializer, EventStoreDatabase database) : IEventStore
{
    private readonly string _addEventsCommandText = $"""
        insert into {database.SchemaName ?? "public"}.events (aggregate_id, version, aggregate_type, type, data)
        select @AggregateId, unnest(@VersionList), @AggregateType, unnest(@TypeList), unnest(@DataList);
        """;
    private readonly string _getEventsCommandText = $"""
        select type, data from {database.SchemaName ?? "public"}.events
        where aggregate_id = @AggregateId and aggregate_type = @AggregateType and version >= @MinVersion
        order by version;
        """;
    private readonly string _hasEventsCommandText = $"""
        select exists (
            select * from {database.SchemaName ?? "public"}.events
            where aggregate_id = @AggregateId and aggregate_type = @AggregateType
        );
        """;

    public async Task AddEvents(
        AggregateDescriptor descriptor,
        long minVersion,
        IEnumerable<EventEnvelope> events,
        CancellationToken cancellationToken)
    {
        var eventCount = (events as IReadOnlyCollection<EventEnvelope>)?.Count;
        var versionList = new List<long>(eventCount ?? 0);
        var typeList = new List<string>(eventCount ?? 0);
        var dataList = new List<byte[]>(eventCount ?? 0);
        var version = minVersion;

        foreach (var evt in events)
        {
            versionList.Add(version++);
            typeList.Add(evt.Type);
            dataList.Add(serializer.Serialize(evt.Type, evt.Data));
        }

        await using var connection = database.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = _addEventsCommandText;
        command.Parameters.Add(new NpgsqlParameter("AggregateId", NpgsqlDbType.Text)
        {
            Value = descriptor.Id
        });
        command.Parameters.Add(new NpgsqlParameter("VersionList", NpgsqlDbType.Bigint | NpgsqlDbType.Array)
        {
            Value = versionList
        });
        command.Parameters.Add(new NpgsqlParameter("AggregateType", NpgsqlDbType.Text)
        {
            Value = descriptor.Type
        });
        command.Parameters.Add(new NpgsqlParameter("TypeList", NpgsqlDbType.Text | NpgsqlDbType.Array)
        {
            Value = typeList
        });
        command.Parameters.Add(new NpgsqlParameter("DataList", NpgsqlDbType.Bytea | NpgsqlDbType.Array)
        {
            Value = dataList
        });
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task<List<EventEnvelope>> GetEvents(
        AggregateDescriptor descriptor,
        long minVersion,
        CancellationToken cancellationToken)
    {
        var events = new List<EventEnvelope>();

        await using var connection = database.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = _getEventsCommandText;
        command.Parameters.Add(new NpgsqlParameter("AggregateId", NpgsqlDbType.Text)
        {
            Value = descriptor.Id
        });
        command.Parameters.Add(new NpgsqlParameter("AggregateType", NpgsqlDbType.Text)
        {
            Value = descriptor.Type
        });
        command.Parameters.Add(new NpgsqlParameter("MinVersion", NpgsqlDbType.Bigint)
        {
            Value = minVersion
        });
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            var type = (string)reader["type"];
            var data = (byte[])reader["data"];
            events.Add(new EventEnvelope(type, serializer.Deserialize(type, data)));
        }

        return events;
    }

    public async Task<bool> HasEvents(AggregateDescriptor descriptor, CancellationToken cancellationToken)
    {
        await using var connection = database.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = _hasEventsCommandText;
        command.Parameters.Add(new NpgsqlParameter("AggregateId", NpgsqlDbType.Text)
        {
            Value = descriptor.Id
        });
        command.Parameters.Add(new NpgsqlParameter("AggregateType", NpgsqlDbType.Text)
        {
            Value = descriptor.Type
        });
        var exists = await command.ExecuteScalarAsync(cancellationToken)
            ?? throw new InvalidOperationException("Select exists returned null.");
        return (bool)exists;
    }
}
