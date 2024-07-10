using System.Text.Json.Nodes;
using Spp.Common.OpenApiGenerator.Core.Exceptions;

namespace Spp.Common.OpenApiGenerator.Core.References;

public readonly struct JsonPointer
{
    private readonly Json.Pointer.JsonPointer _value;

    private JsonPointer(Json.Pointer.JsonPointer value)
    {
        _value = value;
    }

    public override string ToString()
    {
        return _value.ToString();
    }

    public JsonNode? Evaluate(JsonNode? node)
    {
        return _value.TryEvaluate(node, out var result)
            ? result
            : throw new ReferencedObjectNotFoundException($"Could not resolve JSON pointer '{this}'");
    }

    public static JsonPointer Parse(string value)
    {
        return new JsonPointer(Json.Pointer.JsonPointer.Parse(value));
    }
}
