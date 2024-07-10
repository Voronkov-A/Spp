using System;
using System.Linq;
using System.Text.Json.Nodes;

namespace Spp.Common.OpenApiGenerator.Core;

public static class JsonNodeExtensions
{
    public static void Merge(this JsonObject jsonBase, JsonObject? jsonMerge)
    {
        Merge((JsonNode) jsonBase, jsonMerge);
    }

    public static void Merge(this JsonNode jsonBase, JsonNode? jsonMerge)
    {
        if (jsonMerge == null)
        {
            return;
        }

        switch (jsonBase)
        {
            case JsonObject jsonBaseObj when jsonMerge is JsonObject jsonMergeObj:
            {
                var mergeNodesArray = jsonMergeObj.ToArray();
                jsonMergeObj.Clear();

                foreach (var prop in mergeNodesArray)
                {
                    if (jsonBaseObj[prop.Key] is JsonObject jsonBaseChildObj
                        && prop.Value is JsonObject jsonMergeChildObj)
                    {
                        jsonBaseChildObj.Merge(jsonMergeChildObj);
                    }
                    else
                    {
                        jsonBaseObj[prop.Key] = prop.Value;
                    }
                }

                break;
            }
            case JsonArray jsonBaseArray when jsonMerge is JsonArray jsonMergeArray:
            {
                var mergeNodesArray = jsonMergeArray.ToArray();
                jsonMergeArray.Clear();

                foreach (var mergeNode in mergeNodesArray)
                {
                    jsonBaseArray.Add(mergeNode);
                }

                break;
            }
            default:
            {
                throw new ArgumentException(
                    $"""
                    The JsonNode type [{jsonBase.GetType().Name}] is incompatible for merging with the target/base type
                    [{jsonMerge.GetType().Name}]; merge requires the types to be the same.
                    """);
            }
        }
    }
}
