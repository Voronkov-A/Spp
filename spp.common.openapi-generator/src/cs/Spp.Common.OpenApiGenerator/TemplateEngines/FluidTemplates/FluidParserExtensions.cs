using Fluid;
using Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Operators;

namespace Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates;

public static class FluidParserExtensions
{
    public static FluidParser WithBinaryOperator(this FluidParser parser, IBinaryOperator op)
    {
        parser.RegisteredOperators[op.Name] = op.CreateExpression;
        return parser;
    }
}
