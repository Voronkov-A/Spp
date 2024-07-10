using Npgsql;
using NpgsqlTypes;
using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Postgres;

public class PostgresDatabase(IConnectionFactory connectionFactory, ConnectionSettings connectionSettings) : IDatabase
{
    private readonly string _connectionString = connectionSettings.CreateConnectionString();

    public DbConnection CreateConnection()
    {
        return connectionFactory.CreateConnection(_connectionString);
    }

    public async Task EnsureExists(CancellationToken cancellationToken)
    {
        var connectionString = connectionSettings.CreateMasterConnectionString();
        var connection = connectionFactory.CreateConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        if (!await Exists(connection, cancellationToken))
        {
            await TryCreate(connection, cancellationToken);
        }
    }

    public async Task DropIfExists(CancellationToken cancellationToken)
    {
        var connectionString = connectionSettings.CreateMasterConnectionString();
        var connection = connectionFactory.CreateConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        await KillSessions(connection, cancellationToken);
        await DropIfExists(connection, cancellationToken);
    }

    private async Task<bool> Exists(DbConnection masterConnection, CancellationToken cancellationToken)
    {
        await using var command = masterConnection.CreateCommand();
        command.CommandText = "select exists (select * from pg_database where datname = @Database);";
        command.Parameters.Add(new NpgsqlParameter("Database", NpgsqlDbType.Text)
        {
            Value = GetDatabaseName()
        });
        var exists = await command.ExecuteScalarAsync(cancellationToken)
            ?? throw new InvalidOperationException("Select exists returned null.");
        return (bool)exists;
    }

    private async Task TryCreate(DbConnection masterConnection, CancellationToken cancellationToken)
    {
        await using var command = masterConnection.CreateCommand();
        command.CommandText
            = $"create database \"{GetDatabaseName()}\" with encoding = 'UTF8' connection limit = -1;";

        try
        {
            await command.ExecuteScalarAsync(cancellationToken);
        }
        catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.DuplicateDatabase)
        {
            // pass
        }
    }

    private async Task KillSessions(DbConnection masterConnection, CancellationToken cancellationToken)
    {
        await using var command = masterConnection.CreateCommand();
        command.CommandText = """
            select pg_terminate_backend(pid)
            from pg_stat_activity
            where datname = @Database and pid <> pg_backend_pid();
            """;
        command.Parameters.Add(new NpgsqlParameter("Database", NpgsqlDbType.Text)
        {
            Value = GetDatabaseName()
        });
        await command.ExecuteScalarAsync(cancellationToken);
    }

    private async Task DropIfExists(DbConnection masterConnection, CancellationToken cancellationToken)
    {
        await using var command = masterConnection.CreateCommand();
        command.CommandText = $"drop database if exists \"{GetDatabaseName()}\";";
        await command.ExecuteScalarAsync(cancellationToken);
    }

    private string GetDatabaseName()
    {
        return new NpgsqlConnectionStringBuilder(_connectionString).Database
            ?? throw new InvalidOperationException("Database is not specified.");
    }
}
