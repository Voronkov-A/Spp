using System.Collections.Generic;
using System.Text.Json.Nodes;
using Spp.Common.OpenApiGenerator.Core.References;

namespace Spp.Common.OpenApiGenerator.ReferencePreprocessing;

public class ReferenceBasedRootedJsonReferenceRegistry : IRootedJsonReferenceRegistry
{
    private readonly Dictionary<JsonObject, RootedJsonReference> _rootedJsonReferences = new();

    public void Set(JsonObject node, RootedJsonReference reference)
    {
        _rootedJsonReferences[node] = reference;
    }

    public RootedJsonReference Get(JsonObject node)
    {
        return _rootedJsonReferences[node];
    }
}
