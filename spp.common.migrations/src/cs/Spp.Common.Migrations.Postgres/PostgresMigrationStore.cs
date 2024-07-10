using Microsoft.Extensions.Options;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Postgres;

namespace Spp.Common.Migrations.Postgres;

public class PostgresMigrationStore(IDatabase database, IOptions<PostgresMigrationStoreOptions> options) :
    IMigrationStore
{
    private readonly string _initializeCommandText = $"""
        create schema if not exists {options.Value.SchemaName};

        create table if not exists {options.Value.SchemaName}.Migrations (
            Index int not null,
            constraint PK_Migrations primary key (Index)
        );
        """;
    private readonly string _addMigrationCommandText = $"""
        insert into {options.Value.SchemaName}.Migrations (Index) values (@Index);
        """;
    private readonly string _findLastMigrationIndexCommandText = $"""
        select max(Index) from {options.Value.SchemaName}.Migrations;
        """;

    public async Task Initialize(CancellationToken cancellationToken)
    {
        await database.EnsureExists(cancellationToken);
        await using var connection = database.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = _initializeCommandText;
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task AddMigration(IMigration migration, CancellationToken cancellationToken)
    {
        await using var connection = database.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = _addMigrationCommandText;
        command.Parameters.Add(new NpgsqlParameter("Index", NpgsqlDbType.Integer)
        {
            Value = migration.Index
        });
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task<int?> FindLastMigrationIndex(CancellationToken cancellationToken)
    {
        await using var connection = database.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = _findLastMigrationIndexCommandText;
        var lastMigrationIndex = await command.ExecuteScalarAsync(cancellationToken);
        return lastMigrationIndex == DBNull.Value ? null : Convert.ToInt32(lastMigrationIndex);
    }
}

public class PostgresMigrationStore<T>(T database, IOptions<PostgresMigrationStoreOptions<T>> options) :
    PostgresMigrationStore(database, options),
    IMigrationStore<T>
    where T : IDatabase;
