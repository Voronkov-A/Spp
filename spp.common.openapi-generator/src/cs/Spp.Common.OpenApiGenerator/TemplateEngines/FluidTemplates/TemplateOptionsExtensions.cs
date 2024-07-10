using Fluid;
using Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Filters;
using Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Values;

namespace Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates;

public static class FilterCollectionExtensions
{
    public static TemplateOptions WithFilter(this TemplateOptions options, IFilter filter)
    {
        options.Filters.AddFilter(filter.Name, filter.EvaluateAsync);
        return options;
    }

    public static TemplateOptions WithType(this TemplateOptions options, IValueConverter converter)
    {
        options.ValueConverters.Insert(0, converter.Convert);
        return options;
    }
}
