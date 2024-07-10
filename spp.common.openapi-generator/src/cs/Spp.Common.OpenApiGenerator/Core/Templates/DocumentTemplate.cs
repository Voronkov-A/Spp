namespace Spp.Common.OpenApiGenerator.Core.Templates;

public class DocumentTemplate<TParameters>(string path)
{
    public string Path { get; } = path;

    public virtual string GenerateOutputPath(GenerationContext context, TParameters parameters)
    {
        return Path;
    }
}
