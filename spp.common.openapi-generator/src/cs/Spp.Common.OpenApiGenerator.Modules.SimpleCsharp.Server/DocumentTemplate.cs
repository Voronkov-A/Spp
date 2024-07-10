using Spp.Common.OpenApiGenerator.Core;
using Spp.Common.OpenApiGenerator.Core.Templates;

namespace Spp.Common.OpenApiGenerator.Modules.SimpleCsharp.Server;

public class DocumentTemplate() : DocumentTemplate<SimpleCsharpServerModuleParameters>("document.liquid")
{
    public override string GenerateOutputPath(
        GenerationContext document,
        SimpleCsharpServerModuleParameters parameters)
    {
        return parameters.OutputPath;
    }
}
