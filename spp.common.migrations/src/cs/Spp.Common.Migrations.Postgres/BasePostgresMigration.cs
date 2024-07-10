using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Spp.Common.Postgres;

namespace Spp.Common.Migrations.Postgres;

public abstract class BasePostgresMigration(IDatabase database) : IMigration
{
    public abstract int Index { get; }

    protected abstract string UpSql { get; }

    protected virtual string? DownSql => null;

    public async Task Up(CancellationToken cancellationToken)
    {
        await Execute(UpSql, cancellationToken);
    }

    public async Task Down(CancellationToken cancellationToken)
    {
        if (DownSql == null)
        {
            throw new NotSupportedException("Down migration is not supported.");
        }

        await Execute(DownSql, cancellationToken);
    }

    private async Task Execute(string sql, CancellationToken cancellationToken)
    {
        await using var connection = database.CreateConnection();
        var command = new CommandDefinition(sql, cancellationToken: cancellationToken);
        await connection.ExecuteAsync(command);
    }
}
