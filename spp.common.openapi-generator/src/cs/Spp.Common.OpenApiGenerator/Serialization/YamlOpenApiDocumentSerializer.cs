using System.Text;
using System.Text.Json.Nodes;
using YamlDotNet.Serialization;
using YamlDotNet.System.Text.Json;

namespace Spp.Common.OpenApiGenerator.Serialization;

public class YamlOpenApiDocumentSerializer(Encoding encoding) : IOpenApiDocumentSerializer
{
    private readonly IDeserializer _serializer = new DeserializerBuilder()
        .IgnoreUnmatchedProperties()
        .WithTypeConverter(new SystemTextJsonYamlTypeConverter())
        .WithTypeInspector(x => new SystemTextJsonTypeInspector(x))
        .Build();

    public YamlOpenApiDocumentSerializer() : this(new UTF8Encoding(false))
    {
    }

    public JsonNode? Deserialize(byte[] bytes)
    {
        return _serializer.Deserialize<JsonNode?>(encoding.GetString(bytes));
    }
}
