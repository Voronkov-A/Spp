using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Spp.Common.Domain;

public class AggregateFactory<T> : IAggregateFactory<T> where T : BaseAggregate
{
    public static readonly AggregateFactory<T> Instance = new();

    private readonly Func<EntityId, T> _instanceFactory;
    private readonly Action<T, object> _eventPlayer;

    protected AggregateFactory()
    {
        _instanceFactory = CreateInstanceFactory();
        _eventPlayer = CreateEventPlayer();
    }

    public T Create(EntityId id, IEnumerable<object> events)
    {
        var instance = _instanceFactory(id);

        foreach (var evt in events)
        {
            _eventPlayer(instance, evt);
        }

        return instance;
    }

    private static Func<EntityId, T> CreateInstanceFactory()
    {
        var constructor = typeof(T)
            .GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .SingleOrDefault(x =>
                x.GetParameters().Length == 1
                && x.GetParameters()[0].ParameterType == typeof(EntityId))
            ?? throw new InvalidOperationException($"Could not find constructor '{typeof(T)}({typeof(EntityId)})'.");
        var idParameter = Expression.Parameter(typeof(EntityId));
        var constructorInvocation = Expression.New(constructor, idParameter);
        return Expression.Lambda<Func<EntityId, T>>(constructorInvocation, idParameter).Compile();
    }

    private static Action<T, object> CreateEventPlayer()
    {
        var playEventMethod = typeof(BaseAggregate)
            .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
            .Single(x =>
                x.Name == "PlayEvent"
                && x.GetParameters().Length == 1
                && x.GetParameters()[0].ParameterType == typeof(object));
        var instanceParameter = Expression.Parameter(typeof(T));
        var eventParameter = Expression.Parameter(typeof(object));
        var playEventInvocation = Expression.Call(instanceParameter, playEventMethod, eventParameter);
        return Expression.Lambda<Action<T, object>>(playEventInvocation, instanceParameter, eventParameter).Compile();
    }
}
