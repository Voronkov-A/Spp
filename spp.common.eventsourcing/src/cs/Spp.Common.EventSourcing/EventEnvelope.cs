namespace Spp.Common.EventSourcing;

public readonly struct EventEnvelope(string type, object data)
{
    public string Type { get; } = type;

    public object Data { get; } = data;
}
