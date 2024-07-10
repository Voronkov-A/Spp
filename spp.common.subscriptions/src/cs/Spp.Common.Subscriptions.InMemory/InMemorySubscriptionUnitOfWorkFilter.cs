using Spp.Common.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Mediator;
using Spp.Common.Transactions;

namespace Spp.Common.Subscriptions.InMemory;

public class InMemorySubscriptionUnitOfWorkFilter<TSubscription>(TSubscription subscription, IMediator mediator) :
    IUnitOfWorkFilter
    where TSubscription : IEventSubscription
{
    public async Task Commit(IEnumerable<IAggregate> aggregates, CancellationToken cancellationToken)
    {
        foreach (var aggregate in aggregates)
        {
            if (!subscription.AggregateTypeNames.Contains(aggregate.GetType().Name))
            {
                continue;
            }

            foreach (var evt in aggregate.UncommittedEvents)
            {
                await subscription.Dispatch(new EventEnvelope(aggregate.Id, evt), mediator, cancellationToken);
            }
        }
    }
}
