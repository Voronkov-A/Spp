using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.OpenApiGenerator.Core;
using Spp.Common.OpenApiGenerator.Core.References;
using Spp.Common.OpenApiGenerator.Core.Templates;

namespace Spp.Common.OpenApiGenerator;

public class Generator<TParameters>(
    IModule<TParameters> module,
    IDocumentResolver documentResolver,
    IGenerationOutputWriter outputWriter)
{
    public async Task Generate(
        string openApiDocumentPath,
        TParameters parameters,
        CancellationToken cancellationToken)
    {
        var context = await documentResolver.ResolveDocument(
            DocumentPath.Parse(openApiDocumentPath),
            cancellationToken);

        foreach (var template in module.Templates.Document)
        {
            await GenerateDocument(context, parameters, template, cancellationToken);
        }
    }

    private async Task GenerateDocument(
        GenerationContext context,
        TParameters parameters,
        DocumentTemplate<TParameters> template,
        CancellationToken cancellationToken)
    {
        var templateContent = await module.TemplateReader.ReadTemplate(template.Path, cancellationToken);
        var parametersJsonObject = (JsonObject?) JsonSerializer.SerializeToNode(parameters)
                                   ?? throw new InvalidOperationException("SerializeToNode returned null.");
        var input = new GenerationInput(context, parametersJsonObject);
        var output = await module.OutputGenerator.GenerateOutput(templateContent, input, cancellationToken);
        var outputPath = template.GenerateOutputPath(context, parameters);
        await outputWriter.WriteOutput(outputPath, output, cancellationToken);
    }
}
