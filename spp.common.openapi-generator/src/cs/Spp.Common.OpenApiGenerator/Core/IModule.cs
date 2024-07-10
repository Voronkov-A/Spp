namespace Spp.Common.OpenApiGenerator.Core;

public interface IModule<TParameters>
{
    TemplateCollection<TParameters> Templates { get; }

    ITemplateReader TemplateReader { get; }

    IOutputGenerator OutputGenerator { get; }
}
