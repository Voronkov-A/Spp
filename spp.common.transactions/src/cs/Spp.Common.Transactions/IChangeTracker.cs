using Spp.Common.Domain;
using System.Collections.Generic;

namespace Spp.Common.Transactions;

public interface IChangeTracker
{
    IReadOnlyCollection<IAggregate> TrackedAggregates { get; }

    T? FindTrackedAggregate<T>(EntityId id);

    void Track(IAggregate aggregate);
}
