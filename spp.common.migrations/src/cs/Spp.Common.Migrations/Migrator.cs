using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Spp.Common.Migrations;

public class Migrator : IMigrator
{
    private readonly IMigrationStore _migrationStore;
    private readonly List<IMigration> _migrations;
    private readonly ILogger _logger;

    public Migrator(IMigrationStore migrationStore, IEnumerable<IMigration> migrations, ILogger logger)
    {
        _migrationStore = migrationStore;
        _migrations = migrations.OrderBy(x => x.Index).ToList();
        _logger = logger;

        for (var i = 1; i < _migrations.Count; ++i)
        {
            var previous = _migrations[i - 1];
            var current = _migrations[i];

            if (previous.Index == current.Index)
            {
                throw new InvalidOperationException(
                    $"Migrations {previous.GetType()} and {current.GetType()} have the same index {current.Index}.");
            }
        }
    }

    public async Task Up(CancellationToken cancellationToken)
    {
        await _migrationStore.Initialize(cancellationToken);

        if (_migrations.Count == 0)
        {
            return;
        }

        var lastMigrationIndex = await _migrationStore.FindLastMigrationIndex(cancellationToken);

        using var transaction = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            },
            TransactionScopeAsyncFlowOption.Enabled);

        var firstMigrationIndex = lastMigrationIndex == null
            ? 0
            : _migrations.FindIndex(x => x.Index > lastMigrationIndex);

        if (firstMigrationIndex >= 0)
        {
            for (var i = firstMigrationIndex; i < _migrations.Count; ++i)
            {
                var migration = _migrations[i];

                try
                {
                    await migration.Up(cancellationToken);
                }
                catch (Exception)
                {
                    _logger.LogError(
                        "Failed to apply migration {Migration} with index {Index}.",
                        migration.GetType().ToString(),
                        i);
                    throw;
                }

                await _migrationStore.AddMigration(migration, cancellationToken);
            }
        }

        transaction.Complete();
    }
}
