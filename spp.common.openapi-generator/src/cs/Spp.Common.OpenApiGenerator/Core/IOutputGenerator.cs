using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.OpenApiGenerator.Core;

public interface IOutputGenerator
{
    Task<string> GenerateOutput(string templateContent, GenerationInput input, CancellationToken cancellationToken);
}
