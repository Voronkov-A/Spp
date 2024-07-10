using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using Spp.Common.OpenApiGenerator.Core;

namespace Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates;

public static class InputContext
{
    /*public static JsonObject Create(GenerationInput input)
    {
        var result = (JsonObject?) JsonSerializer.SerializeToNode(input)
                     ?? throw new InvalidOperationException("SerializeToNode returned null.");
        return result;
    }*/

    public static IReadOnlyDictionary<string, object?> Create(GenerationInput input)
    {
        var result = (JsonObject?) JsonSerializer.SerializeToNode(input)
                     ?? throw new InvalidOperationException("SerializeToNode returned null.");
        var resultDict = result.ToDictionary(x => x.Key, x => (object?) x.Value);
        resultDict["this"] = resultDict;
        return resultDict;
    }
}
