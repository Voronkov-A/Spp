using System.Text.Json.Nodes;
using Spp.Common.OpenApiGenerator.Core.References;

namespace Spp.Common.OpenApiGenerator.Core;

public class GenerationContext(RootedJsonReference reference, JsonNode? content)
{
    public RootedJsonReference Reference { get; } = reference;

    public JsonNode? Content { get; } = content;
}
