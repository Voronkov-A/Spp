using Spp.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Spp.Common.EventSourcing;

public class AggregateTypeConfigurationBuilder
{
    private readonly Dictionary<Type, TypeDefinition> _aggregateTypes = new();
    private readonly Dictionary<Type, TypeDefinition> _eventTypes = new();

    public IAggregateTypeConfiguration Build()
    {
        return new AggregateTypeConfiguration(_aggregateTypes.Values, _eventTypes.Values);
    }

    public AggregateTypeConfigurationBuilder WithAggregatesFrom(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(IAggregate))))
        {
            _aggregateTypes[type] = new TypeDefinition(type.Name, type);
        }

        return this;
    }

    public AggregateTypeConfigurationBuilder WithEventsFrom(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            _eventTypes[type] = new TypeDefinition(type.Name, type);
        }

        return this;
    }
}
