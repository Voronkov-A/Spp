namespace Spp.Common.OpenApiGenerator.Core.References;

public static class JsonReferenceExtensions
{
    public static RootedJsonReference ResolveFrom(this JsonReference reference, DocumentPath sourcePath)
    {
        if (reference.DocumentPath == null)
        {
            return new RootedJsonReference(sourcePath, reference.TokenPath);
        }

        var targetPath = reference.DocumentPath.Value.ResolveFrom(sourcePath);
        return new RootedJsonReference(targetPath, reference.TokenPath);
    }
}
