using Spp.Common.Domain;
using System;
using System.Collections.Generic;

namespace Spp.Common.Transactions;

public class ChangeTracker : IChangeTracker
{
    private readonly Dictionary<Key, IAggregate> _trackedAggregates = new();

    public IReadOnlyCollection<IAggregate> TrackedAggregates => _trackedAggregates.Values;

    public T? FindTrackedAggregate<T>(EntityId id)
    {
        return (T?) _trackedAggregates.GetValueOrDefault(new Key(typeof(T), id));
    }

    public void Track(IAggregate aggregate)
    {
        _trackedAggregates[new Key(aggregate.GetType(), aggregate.Id)] = aggregate;
    }

    private readonly record struct Key(Type Type, EntityId Id);
}
