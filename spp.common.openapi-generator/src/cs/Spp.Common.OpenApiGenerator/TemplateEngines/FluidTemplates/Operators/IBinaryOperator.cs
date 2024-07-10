using Fluid.Ast;

namespace Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Operators;

public interface IBinaryOperator
{
    string Name { get; }

    Expression CreateExpression(Expression left, Expression right);
}
