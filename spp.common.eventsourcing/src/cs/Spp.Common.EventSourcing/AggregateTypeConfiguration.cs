using System;
using System.Collections.Generic;
using System.Linq;

namespace Spp.Common.EventSourcing;

public class AggregateTypeConfiguration : IAggregateTypeConfiguration
{
    private readonly Dictionary<Type, string> _aggregateTypeToAggregateTypeName;
    private readonly Dictionary<Type, string> _eventTypeToEventTypeName;
    private readonly Dictionary<string, Type> _eventTypeNameToEventType;

    public AggregateTypeConfiguration(
        IEnumerable<TypeDefinition> aggregateTypes,
        IEnumerable<TypeDefinition> eventTypes)
    {
        _aggregateTypeToAggregateTypeName = aggregateTypes.ToDictionary(x => x.ClrType, x => x.Name);
        _eventTypeToEventTypeName = eventTypes.ToDictionary(x => x.ClrType, x => x.Name);
        _eventTypeNameToEventType = _eventTypeToEventTypeName.ToDictionary(x => x.Value, x => x.Key);
    }

    public Type GetEventType(string typeName)
    {
        return _eventTypeNameToEventType[typeName];
    }

    public string GetEventTypeName(Type type)
    {
        return _eventTypeToEventTypeName[type];
    }

    public string GetTypeName(Type type)
    {
        return _aggregateTypeToAggregateTypeName[type];
    }
}
