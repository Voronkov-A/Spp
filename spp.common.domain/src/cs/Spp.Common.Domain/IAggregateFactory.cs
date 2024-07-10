using System.Collections.Generic;

namespace Spp.Common.Domain;

public interface IAggregateFactory<T>
{
    T Create(EntityId id, IEnumerable<object> events);
}
