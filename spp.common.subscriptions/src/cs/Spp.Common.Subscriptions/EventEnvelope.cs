using Spp.Common.Domain;

namespace Spp.Common.Subscriptions;

public readonly struct EventEnvelope<T>(EntityId aggregateId, T evt)
{
    public EntityId AggregateId { get; } = aggregateId;

    public T Event { get; } = evt;
}

public readonly struct EventEnvelope(EntityId aggregateId, object evt)
{
    public EntityId AggregateId { get; } = aggregateId;

    public object Event { get; } = evt;
}
