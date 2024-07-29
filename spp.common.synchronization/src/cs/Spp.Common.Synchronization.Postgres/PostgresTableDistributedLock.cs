using Dapper;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Spp.Common.Synchronization.Postgres;

internal sealed class PostgresTableDistributedLock(string id, SynchronizationDatabase database) : IDistributedLock
{
    private readonly string _acquireSql = $"""
        insert into {database.SchemaName}.Locks (Id) values (@Id) on conflict (Id) do update set Id = @Id;
        """;
    private readonly string _id = id;
    private readonly SynchronizationDatabase _database = database;
    private DbConnection? _connection;
    private DbTransaction? _transaction;

    public async Task Acquire(CancellationToken cancellationToken)
    {
        if (_transaction != null)
        {
            return;
        }

        try
        {
            using var transactionScope = new TransactionScope(
                TransactionScopeOption.Suppress,
                TransactionScopeAsyncFlowOption.Enabled);

            _connection = _database.CreateConnection();
            await _connection.OpenAsync(cancellationToken);
            _transaction = await _connection.BeginTransactionAsync(
                System.Data.IsolationLevel.Serializable,
                cancellationToken);
            var command = new CommandDefinition(
                _acquireSql,
                new { Id = _id },
                transaction: _transaction,
                cancellationToken: cancellationToken);
            await _connection.ExecuteAsync(command);
        }
        catch
        {
            await ResetState();
            throw;
        }
    }

    public async Task Release(CancellationToken cancellationToken)
    {
        await ResetState();
    }

    public void Dispose()
    {
        DisposeAsync().AsTask().Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await ResetState();
    }

    private async ValueTask ResetState()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        if (_connection != null)
        {
            await _connection.DisposeAsync();
            _connection = null;
        }
    }
}
