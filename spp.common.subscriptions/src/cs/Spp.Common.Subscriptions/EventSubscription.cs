using System;
using System.Collections.Generic;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;
using System.Threading.Tasks;
using System.Threading;

namespace Spp.Common.Subscriptions;

public abstract class EventSubscription : IEventSubscription
{
    private readonly Dictionary<Type, Func<EventEnvelope, IMediator, CancellationToken, Task>> _dispatchers = new();
    private readonly HashSet<string> _aggregateTypeNames = new();

    public IReadOnlySet<string> AggregateTypeNames => _aggregateTypeNames;

    public async Task Dispatch(EventEnvelope evt, IMediator mediator, CancellationToken cancellationToken)
    {
        if (_dispatchers.TryGetValue(evt.Event.GetType(), out var dispatcher))
        {
            await dispatcher(evt, mediator, cancellationToken);
        }
    }

    protected DispatcherConfigurator<TEvent> When<TEvent>(string aggregateTypeName)
    {
        return new DispatcherConfigurator<TEvent>(this, aggregateTypeName);
    }

    protected void AddHandler<TEvent, TRequest>(string aggregateTypeName, Func<EventEnvelope<TEvent>, TRequest> convert)
        where TRequest : IRequest<Unit>
    {
        _aggregateTypeNames.Add(aggregateTypeName);
        _dispatchers[typeof(TEvent)] = async (evt, mediator, ct) =>
        {
            var request = convert(new EventEnvelope<TEvent>(evt.AggregateId, (TEvent)evt.Event));
            await mediator.Dispatch<TRequest, Unit>(request, ct);
        };
    }

    protected readonly struct DispatcherConfigurator<TEvent>(EventSubscription self, string aggregateTypeName)
    {
        public void Dispatch<TRequest>(Func<EventEnvelope<TEvent>, TRequest> convert) where TRequest : IRequest<Unit>
        {
            self.AddHandler(aggregateTypeName, convert);
        }
    }
}
