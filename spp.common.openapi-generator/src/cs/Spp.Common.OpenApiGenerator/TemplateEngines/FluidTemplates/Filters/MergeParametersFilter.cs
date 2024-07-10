using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Fluid;
using Fluid.Values;

namespace Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Filters;

public class MergeParametersFilter : IFilter
{
    public string Name => "merge_parameters";

    public ValueTask<FluidValue> EvaluateAsync(
        FluidValue input,
        FilterArguments arguments,
        TemplateContext context)
    {
        var left = input as ArrayValue;
        var right = arguments.At(0) as ArrayValue;

        if (left == null || right == null)
        {
            var notNull = left == null ? arguments.At(0) : input;
            return ValueTask.FromResult(notNull);
        }

        var leftKeySet = new HashSet<(string, string)>();
        var valuesToAdd = new List<FluidValue>(right.Values.Length);

        foreach (var parameter in left.Values)
        {
            if (parameter.ToObjectValue() is not JsonObject jsonObject)
            {
                continue;
            }

            var @in = jsonObject["in"]?.ToString();
            var name = jsonObject["name"]?.ToString();

            if (@in != null && name != null)
            {
                leftKeySet.Add((@in, name));
            }
        }

        foreach (var parameter in right.Values)
        {
            if (parameter.ToObjectValue() is not JsonObject jsonObject)
            {
                continue;
            }

            var @in = jsonObject["in"]?.ToString();
            var name = jsonObject["name"]?.ToString();

            if (@in != null && name != null && !leftKeySet.Contains((@in, name)))
            {
                valuesToAdd.Add(parameter);
            }
        }

        var result = valuesToAdd.Count == 0
            ? input
            : FluidValue.Create(left.Values.Concat(valuesToAdd), context.Options);
        return ValueTask.FromResult(result);
    }
}
