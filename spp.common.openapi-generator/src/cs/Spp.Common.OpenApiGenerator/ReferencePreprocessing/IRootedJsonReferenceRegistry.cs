using System.Text.Json.Nodes;
using Spp.Common.OpenApiGenerator.Core.References;

namespace Spp.Common.OpenApiGenerator.ReferencePreprocessing;

public interface IRootedJsonReferenceRegistry
{
    void Set(JsonObject node, RootedJsonReference reference);

    RootedJsonReference Get(JsonObject node);
}
