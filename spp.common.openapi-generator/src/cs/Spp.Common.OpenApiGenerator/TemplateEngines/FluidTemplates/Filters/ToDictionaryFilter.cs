using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Fluid;
using Fluid.Values;

namespace Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Filters;

public class ToDictionaryFilter : IFilter
{
    public string Name => "to_dictionary";

    public ValueTask<FluidValue> EvaluateAsync(FluidValue input, FilterArguments arguments, TemplateContext context)
    {
        var objectValue = input.ToObjectValue();

        if (objectValue == null)
        {
            return NilValue.Instance;
        }

        var node = objectValue as JsonNode ?? JsonSerializer.SerializeToNode(objectValue);

        if (node is not JsonObject jsonObject)
        {
            return NilValue.Instance;
        }

        var dictionary = jsonObject.ToDictionary(x => x.Key, x => x.Value);
        var result = FluidValue.Create(dictionary, context.Options);
        return ValueTask.FromResult(result);
    }
}
