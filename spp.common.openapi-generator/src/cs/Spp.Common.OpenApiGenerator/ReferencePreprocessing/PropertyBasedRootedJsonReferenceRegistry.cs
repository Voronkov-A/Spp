using System;
using System.Text.Json.Nodes;
using Spp.Common.OpenApiGenerator.Core.References;

namespace Spp.Common.OpenApiGenerator.ReferencePreprocessing;

public class PropertyBasedRootedJsonReferenceRegistry(PropertyBasedRootedJsonReferenceRehistrySettings settings) :
    IRootedJsonReferenceRegistry
{
    public void Set(JsonObject node, RootedJsonReference reference)
    {
        node[settings.RootedJsonReferenceKey] = JsonValue.Create(reference.ToString());
    }

    public RootedJsonReference Get(JsonObject node)
    {
        var reference = node[settings.RootedJsonReferenceKey]?.ToString()
                        ?? throw new InvalidOperationException("Could not read rooted json reference property.");
        return RootedJsonReference.Parse(reference);
    }
}
