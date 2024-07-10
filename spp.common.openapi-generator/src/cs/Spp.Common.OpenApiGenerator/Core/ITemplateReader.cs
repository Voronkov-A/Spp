using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.OpenApiGenerator.Core;

public interface ITemplateReader
{
    Task<string> ReadTemplate(string path, CancellationToken cancellationToken);
}
