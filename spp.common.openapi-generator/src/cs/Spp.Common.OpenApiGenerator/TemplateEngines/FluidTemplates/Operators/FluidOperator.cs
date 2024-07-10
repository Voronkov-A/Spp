using System.Threading.Tasks;
using Fluid;
using Fluid.Ast;
using Fluid.Values;

namespace Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Operators;

public class FluidOperator(FluidParser parser) : IBinaryOperator
{
    public string Name => "liquid";

    public Expression CreateExpression(Expression left, Expression right)
    {
        return new FluidExpression(parser, left, right);
    }

    private class FluidExpression(FluidParser parser, Expression left, Expression right) : BinaryExpression(left, right)
    {
        public override async ValueTask<FluidValue> EvaluateAsync(TemplateContext context)
        {
            var subContextValue = await Left.EvaluateAsync(context);
            var subTemplateContent = (await Right.EvaluateAsync(context)).ToStringValue();
            var subTemplate = parser.Parse(subTemplateContent);
            var subContext = new TemplateContext(subContextValue, context.Options);
            var result = await subTemplate.RenderAsync(subContext);
            return FluidValue.Create(result, context.Options);
        }
    }
}
