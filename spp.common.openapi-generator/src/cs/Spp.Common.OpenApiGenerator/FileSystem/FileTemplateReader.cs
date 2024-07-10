using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.OpenApiGenerator.Core;

namespace Spp.Common.OpenApiGenerator.FileSystem;

public class FileTemplateReader(string rootDirectoryPath, Encoding encoding) : ITemplateReader
{
    public FileTemplateReader(string rootDirectoryPath) : this(rootDirectoryPath, new UTF8Encoding(false))
    {
    }

    public async Task<string> ReadTemplate(string path, CancellationToken cancellationToken)
    {
        var inputPath = Path.Combine(rootDirectoryPath, path);
        return await File.ReadAllTextAsync(inputPath, encoding, cancellationToken);
    }
}
