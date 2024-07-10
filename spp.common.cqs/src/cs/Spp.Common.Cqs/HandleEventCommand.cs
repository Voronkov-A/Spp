using Spp.Common.Domain;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;
using Spp.Common.Subscriptions;

namespace Spp.Common.Cqs;

public class HandleEventCommand<TEvent>(EventEnvelope<TEvent> envelope) : IRequest<Unit>
{
    public EntityId AggregateId { get; } = envelope.AggregateId;

    public TEvent Event { get; } = envelope.Event;
}
