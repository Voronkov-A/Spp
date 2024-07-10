using System.Threading;
using System.Threading.Tasks;
using Spp.Common.OpenApiGenerator.Core.References;

namespace Spp.Common.OpenApiGenerator.Core;

public interface IDocumentResolver
{
    Task<GenerationContext> ResolveDocument(DocumentPath path, CancellationToken cancellationToken);
}
