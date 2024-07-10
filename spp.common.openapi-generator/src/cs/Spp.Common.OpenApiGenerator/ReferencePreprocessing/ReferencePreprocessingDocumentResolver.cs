using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.OpenApiGenerator.Core;
using Spp.Common.OpenApiGenerator.Core.References;

namespace Spp.Common.OpenApiGenerator.ReferencePreprocessing;

public class ReferencePreprocessingDocumentResolver(
    IDocumentResolver inner,
    IRootedJsonReferenceRegistry registry) :
    IDocumentResolver
{
    public async Task<GenerationContext> ResolveDocument(DocumentPath path, CancellationToken cancellationToken)
    {
        var context = await inner.ResolveDocument(path, cancellationToken);
        RegisterRootedReferences(path, context.Content);
        return context;
    }

    private void RegisterRootedReferences(DocumentPath documentPath, JsonNode? node)
    {
        switch (node)
        {
            case JsonObject jsonObject:
                {
                    RegisterRootedReferences(documentPath, jsonObject);
                    break;
                }
            case JsonArray jsonArray:
                {
                    RegisterRootedReferences(documentPath, jsonArray);
                    break;
                }
        }
    }

    private void RegisterRootedReferences(DocumentPath documentPath, JsonObject node)
    {
        if (node.ContainsKey("$ref"))
        {
            var reference = new RootedJsonReference(
                documentPath,
                JsonPointer.Parse(node.GetPath().TrimStart(new[] { '$', '.' }).Replace('.', '/')));
            registry.Set(node, reference);
            return;
        }

        foreach (var (_, value) in node)
        {
            RegisterRootedReferences(documentPath, value);
        }
    }

    private void RegisterRootedReferences(DocumentPath documentPath, JsonArray node)
    {
        foreach (var item in node)
        {
            RegisterRootedReferences(documentPath, item);
        }
    }
}
