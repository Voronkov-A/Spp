using Spp.Common.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.EventSourcing.EventStore;

public interface IEventStoreRepository : IEventRepository
{
    Task<List<T>> GetAll<T>(
        IEnumerable<EntityId> ids,
        IAggregateFactory<T> aggregateFactory,
        CancellationToken cancellationToken);

    Task<T?> Find<T>(EntityId id, IAggregateFactory<T> aggregateFactory, CancellationToken cancellationToken);

    Task<bool> Exists<T>(EntityId id, CancellationToken cancellationToken);
}
