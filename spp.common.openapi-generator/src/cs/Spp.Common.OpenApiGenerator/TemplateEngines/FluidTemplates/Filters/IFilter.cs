using System.Threading.Tasks;
using Fluid;
using Fluid.Values;

namespace Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Filters;

public interface IFilter
{
    string Name { get; }

    ValueTask<FluidValue> EvaluateAsync(FluidValue input, FilterArguments arguments, TemplateContext context);
}
