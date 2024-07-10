using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.EventSourcing.EventStore;

public interface IEventStore
{
    Task<List<EventEnvelope>> GetEvents(
        AggregateDescriptor descriptor,
        long minVersion,
        CancellationToken cancellationToken);

    Task<bool> HasEvents(AggregateDescriptor descriptor, CancellationToken cancellationToken);

    Task AddEvents(
        AggregateDescriptor descriptor,
        long minVersion,
        IEnumerable<EventEnvelope> events,
        CancellationToken cancellationToken);
}
