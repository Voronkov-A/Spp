using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Fluid;
using Fluid.Values;
using Spp.Common.OpenApiGenerator.Core;
using Spp.Common.OpenApiGenerator.ReferencePreprocessing;

namespace Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Filters;

public class ResolveRefFilter(IDocumentResolver documentResolver, IRootedJsonReferenceRegistry registry) : IFilter
{
    public string Name => "resolve_ref";

    public async ValueTask<FluidValue> EvaluateAsync(
        FluidValue input,
        FilterArguments arguments,
        TemplateContext context)
    {
        var objectValue = input.ToObjectValue();

        if (objectValue == null)
        {
            return input;
        }

        var node = objectValue as JsonNode ?? JsonSerializer.SerializeToNode(objectValue);

        if (node is not JsonObject jsonObject)
        {
            return input;
        }

        var reference = node["$ref"];

        if (reference == null)
        {
            return input;
        }

        var rootedReference = registry.Get(jsonObject);
        var resolvedNode = await documentResolver.ResolveReference(rootedReference, CancellationToken.None);

        if (!(resolvedNode.Content is JsonObject resolvedJsonObject))
        {
            return FluidValue.Create(resolvedNode.Content, context.Options);
        }

        node.Merge(resolvedJsonObject);
        node["$ref"] = JsonValue.Create(reference);
        return FluidValue.Create(resolvedJsonObject, context.Options);
    }
}
