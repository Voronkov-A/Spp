using System.Threading;
using System.Threading.Tasks;
using Spp.Common.OpenApiGenerator.Core.References;

namespace Spp.Common.OpenApiGenerator.Core;

public static class DocumentResolverExtensions
{
    public static async Task<GenerationContext> ResolveReference(
        this IDocumentResolver documentResolver,
        RootedJsonReference reference,
        CancellationToken cancellationToken)
    {
        var document = await documentResolver.ResolveDocument(reference.DocumentPath, cancellationToken);
        return reference.TokenPath == null
            ? document
            : new GenerationContext(
                new RootedJsonReference(document.Reference.DocumentPath, reference.TokenPath),
                reference.TokenPath.Value.Evaluate(document.Content));
    }
}
