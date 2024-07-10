using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Fluid;
using Spp.Common.OpenApiGenerator.Core;
using Spp.Common.OpenApiGenerator.ReferencePreprocessing;
using Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Filters;
using Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Operators;
using Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Values;

namespace Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates;

public class FluidOutputGenerator : IOutputGenerator
{
    private readonly FluidParser _parser;
    private readonly TemplateOptions _options;

    public FluidOutputGenerator(IDocumentResolver documentResolver, IRootedJsonReferenceRegistry registry)
    {
        _parser = new(new FluidParserOptions
        {
            AllowFunctions = true
        });
        //_parser.WithBinaryOperator(new FluidOperator(_parser));
        _options = new TemplateOptions()
            //.WithType(new JsonObjectValueConverter())
            .WithFilter(new PascalCaseFilter())
            .WithFilter(new FluidFilter(_parser))
            .WithFilter(new ToDictionaryFilter())
            .WithFilter(new ResolveRefFilter(documentResolver, registry));
    }

    public async Task<string> GenerateOutput(
        string templateContent,
        GenerationInput input,
        CancellationToken cancellationToken)
    {
        var template = _parser.Parse(templateContent);
        var inputContext = InputContext.Create(input);
        var context = new TemplateContext(inputContext, _options);
        return await template.RenderAsync(context);
    }
}
