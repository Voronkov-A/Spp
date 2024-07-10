using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Fluid;
using Fluid.Values;

namespace Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Filters;

public class IterateFilter : IFilter
{
    public string Name => "iterate";

    public ValueTask<FluidValue> EvaluateAsync(
        FluidValue input,
        FilterArguments arguments,
        TemplateContext context)
    {
        var objectValue = input.ToObjectValue() as JsonObject;

        if (objectValue == null)
        {
            return ArrayValue.Empty;
        }

        var items = objectValue.Select(x => new[] { x.Key, x.Value }).ToList();
        var result = FluidValue.Create(items, context.Options);
        return ValueTask.FromResult(result);
    }
}
