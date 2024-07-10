using System.Collections.Generic;
using System.Linq;
using Spp.Common.OpenApiGenerator.Core.Templates;

namespace Spp.Common.OpenApiGenerator.Core;

public class TemplateCollection<TParameters>(IEnumerable<DocumentTemplate<TParameters>> document)
{
    public IReadOnlyCollection<DocumentTemplate<TParameters>> Document { get; } = document.ToList();
}
