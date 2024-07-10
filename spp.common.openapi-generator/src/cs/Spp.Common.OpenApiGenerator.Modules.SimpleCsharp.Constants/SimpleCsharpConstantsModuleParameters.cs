using System.Text.Json.Serialization;

namespace Spp.Common.OpenApiGenerator.Modules.SimpleCsharp.Constants;

public class SimpleCsharpConstantsModuleParameters
{
    [JsonPropertyName("outputPath")]
    public required string OutputPath { get; init; }

    [JsonPropertyName("namespace")]
    public required string Namespace { get; init; }

    [JsonPropertyName("pathsModelNameTemplate")]
    public required string PathsModelNameTemplate { get; init; }
}
