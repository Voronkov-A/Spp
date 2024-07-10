using Spp.Common.OpenApiGenerator.Core;
using Spp.Common.OpenApiGenerator.FileSystem;
using Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates;

namespace Spp.Common.OpenApiGenerator.Modules.SimpleCsharp.Server;

public class SimpleCsharpServerModule : IModule<SimpleCsharpServerModuleParameters>
{
    public TemplateCollection<SimpleCsharpServerModuleParameters> Templates { get; } = new(
        document: new[]
        {
            new DocumentTemplate()
        });

    public ITemplateReader TemplateReader { get; }
        = new FileTemplateReader("Content/Spp.Common.OpenApiGenerator.Modules.SimpleCsharp.Server");

    public IOutputGenerator OutputGenerator =>
        throw new System.NotImplementedException(); //{ get; } = new FluidOutputGenerator();
}
