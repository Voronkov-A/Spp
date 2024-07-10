namespace Spp.Common.EventSourcing;

public readonly struct AggregateDescriptor(string type, string id)
{
    public string Type { get; } = type;

    public string Id { get; } = id;
}
