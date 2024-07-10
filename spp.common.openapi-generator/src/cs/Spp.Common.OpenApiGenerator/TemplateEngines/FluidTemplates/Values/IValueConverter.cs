namespace Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Values;

public interface IValueConverter
{
    object? Convert(object? value);
}
