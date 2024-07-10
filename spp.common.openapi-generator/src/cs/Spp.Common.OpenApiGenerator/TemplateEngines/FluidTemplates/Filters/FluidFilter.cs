using System.Threading.Tasks;
using Fluid;
using Fluid.Values;

namespace Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Filters;

public class FluidFilter(FluidParser parser) : IFilter
{
    public string Name => "fluid";

    public async ValueTask<FluidValue> EvaluateAsync(
        FluidValue input,
        FilterArguments arguments,
        TemplateContext context)
    {
        var subContextValue = input;
        var subTemplateContent = arguments.At(0).ToStringValue();
        var subTemplate = parser.Parse(subTemplateContent);
        var subContext = new TemplateContext(subContextValue, context.Options);
        var result = await subTemplate.RenderAsync(subContext);
        return FluidValue.Create(result, context.Options);
    }
}
