using System.Collections.Generic;

namespace Spp.Common.Domain;

public abstract class BaseAggregate : IAggregate
{
    private readonly IEventDispatcher _eventDispatcher;
    private readonly List<object> _uncommittedEvents;

    protected BaseAggregate(EntityId id, IEventDispatcher eventDispatcher)
    {
        Id = id;
        Version = 0;
        _uncommittedEvents = new List<object>();
        _eventDispatcher = eventDispatcher;
    }

    public EntityId Id { get; }

    public long Version { get; private set; }

    public IReadOnlyCollection<object> UncommittedEvents => _uncommittedEvents;

    protected void AddEvent(object evt)
    {
        _uncommittedEvents.Add(evt);
        PlayEvent(evt);
    }

    private void PlayEvent(object evt)
    {
        _eventDispatcher.Dispatch(this, evt);
        ++Version;
    }
}
