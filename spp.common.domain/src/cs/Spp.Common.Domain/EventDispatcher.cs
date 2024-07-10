using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Spp.Common.Domain;

public class EventDispatcher<TAggregate> : IEventDispatcher
{
    public static readonly EventDispatcher<TAggregate> Instance = new();

    private readonly Dictionary<Type, Action<TAggregate, object>> _handlers;

    private EventDispatcher()
    {
        var handlerMethods = typeof(TAggregate)
            .GetMethods(
                BindingFlags.Instance
                | BindingFlags.Public
                | BindingFlags.NonPublic
                | BindingFlags.FlattenHierarchy)
            .Where(x =>
                x.Name == "When"
                && !x.IsGenericMethod
                && !x.IsGenericMethodDefinition
                && x.ReturnType == typeof(void)
                && x.GetParameters().Length == 1
                && !x.GetParameters()[0].ParameterType.IsAbstract);
        _handlers = handlerMethods.ToDictionary(x => x.GetParameters()[0].ParameterType, CreateHandler);
    }

    public void Dispatch(object aggregate, object evt)
    {
        if (_handlers.TryGetValue(evt.GetType(), out var handler))
        {
            handler((TAggregate)aggregate, evt);
        }
        else
        {
            throw new InvalidOperationException($"Could not find method '{typeof(TAggregate)}.When({evt.GetType()})'.");
        }
    }

    private static Action<TAggregate, object> CreateHandler(MethodInfo handlerMethod)
    {
        var eventType = handlerMethod.GetParameters()[0].ParameterType;
        var instanceParameter = Expression.Parameter(typeof(TAggregate));
        var eventParameter = Expression.Parameter(typeof(object));
        var castEventExpression = Expression.Convert(eventParameter, eventType);
        var invocationExpression = Expression.Call(instanceParameter, handlerMethod, castEventExpression);
        var expression = Expression.Lambda<Action<TAggregate, object>>(
            invocationExpression,
            instanceParameter,
            eventParameter);
        return expression.Compile();
    }
}
