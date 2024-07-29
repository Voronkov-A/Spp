using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spp.Common.Miscellaneous.Serialization;

public static class DefaultJsonSerializer
{
    public static JsonSerializerOptions Options { get; } = CreateOptions();

    private static JsonSerializerOptions CreateOptions()
    {
        var result = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        result.Converters.Add(new JsonStringEnumConverter());
        return result;
    }
}
