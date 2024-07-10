using Spp.Common.OpenApiGenerator.Core;
using Spp.Common.OpenApiGenerator.Core.Templates;

namespace Spp.Common.OpenApiGenerator.Modules.SimpleCsharp.Constants;

public class DocumentTemplate() : DocumentTemplate<SimpleCsharpConstantsModuleParameters>("document.liquid")
{
    public override string GenerateOutputPath(
        GenerationContext context,
        SimpleCsharpConstantsModuleParameters parameters)
    {
        return parameters.OutputPath;
    }
}
