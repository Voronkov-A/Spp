using System.Text.Json.Nodes;

namespace Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Values;

public class JsonObjectValueConverter : IValueConverter
{
    public object? Convert(object? value)
    {
        return value is JsonObject jsonObject ? new JsonObjectValue(jsonObject) : null;
    }
}
