using System;

namespace Spp.Common.EventSourcing;

public interface IAggregateTypeConfiguration
{
    string GetTypeName(Type type);

    string GetEventTypeName(Type type);

    Type GetEventType(string typeName);
}
