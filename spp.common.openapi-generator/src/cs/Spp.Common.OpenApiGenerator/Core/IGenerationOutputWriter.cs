using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.OpenApiGenerator.Core;

public interface IGenerationOutputWriter
{
    Task WriteOutput(string path, string output, CancellationToken cancellationToken);
}
