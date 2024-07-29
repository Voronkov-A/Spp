using Dapper;
using Npgsql;
using Spp.Common.Initialization;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Synchronization.Postgres;

public class PostgresTableDistributedLockInitializer(SynchronizationDatabase database) : IInitializer
{
    private readonly SynchronizationDatabase _database = database;

    public async Task Initialize(CancellationToken cancellationToken)
    {
        await _database.EnsureExists(cancellationToken);
        await EnsureSchemaExists(cancellationToken);
        await EnsureTableExists(cancellationToken);
    }

    private async Task EnsureSchemaExists(CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(
            $"create schema if not exists {_database.SchemaName};",
            cancellationToken: cancellationToken);

        try
        {
            await using var connection = _database.CreateConnection();
            await connection.ExecuteAsync(command);
        }
        catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.DuplicateSchema)
        {
            await using var connection = _database.CreateConnection();
            await connection.ExecuteAsync(command);
        }
    }

    private async Task EnsureTableExists(CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(
            $"create table if not exists {_database.SchemaName}.Locks (Id text primary key);",
            cancellationToken: cancellationToken);

        try
        {
            await using var connection = _database.CreateConnection();
            await connection.ExecuteAsync(command);
        }
        catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.DuplicateTable)
        {
            await using var connection = _database.CreateConnection();
            await connection.ExecuteAsync(command);
        }
    }
}
