using System;
using System.Text.Json;

namespace Spp.Common.EventSourcing;

public class JsonEventSerializer(IAggregateTypeConfiguration configuration) : IEventSerializer
{
    public object Deserialize(string type, byte[] bytes)
    {
        var clrType = configuration.GetEventType(type);
        return JsonSerializer.Deserialize(bytes, clrType)
            ?? throw new InvalidOperationException("Deserialized to null.");
    }

    public byte[] Serialize(string type, object evt)
    {
        var clrType = configuration.GetEventType(type);
        return JsonSerializer.SerializeToUtf8Bytes(evt, clrType);
    }
}
