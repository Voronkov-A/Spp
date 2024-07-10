using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Domain;

namespace Spp.Common.EventSourcing.EventStore;

public class EventStoreRepository(IEventStore eventStore, IAggregateTypeConfiguration configuration) :
    IEventStoreRepository
{
    public async Task<bool> Exists<T>(EntityId id, CancellationToken cancellationToken)
    {
        return await eventStore.HasEvents(
            new AggregateDescriptor(configuration.GetTypeName(typeof(T)), id.ToString()),
            cancellationToken);
    }

    public async Task<List<T>> GetAll<T>(
        IEnumerable<EntityId> ids,
        IAggregateFactory<T> aggregateFactory,
        CancellationToken cancellationToken)
    {
        var items = new List<T>();

        foreach (var id in ids)
        {
            var item = await Find<T>(id, aggregateFactory, cancellationToken);

            if (item != null)
            {
                items.Add(item);
            }
        }

        return items;
    }

    public async Task<T?> Find<T>(
        EntityId id,
        IAggregateFactory<T> aggregateFactory,
        CancellationToken cancellationToken)
    {
        var events = await eventStore.GetEvents(
            new AggregateDescriptor(configuration.GetTypeName(typeof(T)), id.ToString()),
            0,
            cancellationToken);
        return events.Count == 0 ? default : aggregateFactory.Create(id, events.Select(x => x.Data));
    }

    public async Task Save(IAggregate aggregate, CancellationToken cancellationToken)
    {
        await eventStore.AddEvents(
            new AggregateDescriptor(configuration.GetTypeName(aggregate.GetType()), aggregate.Id.ToString()),
            aggregate.Version - aggregate.UncommittedEvents.Count + 1,
            aggregate.UncommittedEvents.Select(x => new EventEnvelope(configuration.GetEventTypeName(x.GetType()), x)),
            cancellationToken);
    }
}
