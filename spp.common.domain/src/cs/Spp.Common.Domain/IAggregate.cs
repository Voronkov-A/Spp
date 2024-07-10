using System.Collections.Generic;

namespace Spp.Common.Domain;

public interface IAggregate
{
    EntityId Id { get; }

    long Version { get; }

    IReadOnlyCollection<object> UncommittedEvents { get; }
}
