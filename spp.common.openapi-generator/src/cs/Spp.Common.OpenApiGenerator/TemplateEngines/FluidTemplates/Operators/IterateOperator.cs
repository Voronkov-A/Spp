using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Fluid;
using Fluid.Ast;
using Fluid.Values;

namespace Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Operators;

public class IterateOperator(Expression argument) : Expression
{
    public override async ValueTask<FluidValue> EvaluateAsync(TemplateContext context)
    {
        var objectValue = (await argument.EvaluateAsync(context)).ToObjectValue() as JsonObject;

        if (objectValue == null)
        {
            return ArrayValue.Empty;
        }

        var items = objectValue.Select(x => new[] { x.Key, x.Value }).ToList();
        return FluidValue.Create(items, context.Options);
    }
}
