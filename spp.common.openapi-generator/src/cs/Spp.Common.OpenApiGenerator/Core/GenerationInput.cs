using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Spp.Common.OpenApiGenerator.Core;

public class GenerationInput(GenerationContext context, JsonObject parameters)
{
    [JsonPropertyName("context")]
    public GenerationContext Context { get; } = context;

    [JsonPropertyName("parameters")]
    public JsonObject Parameters { get; } = parameters;
}
