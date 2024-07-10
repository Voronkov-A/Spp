using Spp.Common.OpenApiGenerator.Core;
using Spp.Common.OpenApiGenerator.FileSystem;
using Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates;

namespace Spp.Common.OpenApiGenerator.Modules.SimpleCsharp.Constants;

public class SimpleCsharpConstantsModule : IModule<SimpleCsharpConstantsModuleParameters>
{
    public TemplateCollection<SimpleCsharpConstantsModuleParameters> Templates { get; } = new(
        document: new[]
        {
            new DocumentTemplate()
        });

    public ITemplateReader TemplateReader { get; }
        = new FileTemplateReader("Content/Spp.Common.OpenApiGenerator.Modules.SimpleCsharp.Constants");

    public IOutputGenerator OutputGenerator =>
        throw new System.NotImplementedException();  //{ get; } = new FluidOutputGenerator();
}
