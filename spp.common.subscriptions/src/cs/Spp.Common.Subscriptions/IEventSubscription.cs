using Spp.Common.Mediator;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Spp.Common.Subscriptions;

public interface IEventSubscription
{
    IReadOnlySet<string> AggregateTypeNames { get; }

    Task Dispatch(EventEnvelope evt, IMediator mediator, CancellationToken cancellationToken);
}
