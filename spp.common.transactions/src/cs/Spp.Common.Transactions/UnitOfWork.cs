using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Spp.Common.Transactions;

public class UnitOfWork(IChangeTracker changeTracker, IEnumerable<IUnitOfWorkFilter> filters) : IUnitOfWork
{
    private readonly List<IUnitOfWorkFilter> _filters = filters.ToList();
    private bool _committed;

    public async Task Commit(CancellationToken cancellationToken)
    {
        if (_committed)
        {
            throw new InvalidOperationException("Already committed.");
        }

        var aggregates = changeTracker.TrackedAggregates.Where(x => x.UncommittedEvents.Count > 0);

        using var transaction = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            },
            TransactionScopeAsyncFlowOption.Enabled);

        foreach (var filter in _filters)
        {
            await filter.Commit(aggregates, cancellationToken);
        }

        transaction.Complete();
        _committed = true;
    }
}
