using System.Text.Json.Nodes;

namespace Spp.Common.OpenApiGenerator.Serialization;

public interface IOpenApiDocumentSerializer
{
    JsonNode? Deserialize(byte[] bytes);
}
