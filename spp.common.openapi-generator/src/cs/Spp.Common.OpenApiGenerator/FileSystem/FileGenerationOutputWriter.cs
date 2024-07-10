using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.OpenApiGenerator.Core;

namespace Spp.Common.OpenApiGenerator.FileSystem;

public class FileGenerationOutputWriter(string rootDirectoryPath, Encoding encoding) : IGenerationOutputWriter
{
    public FileGenerationOutputWriter(string rootDirectoryPath) : this(rootDirectoryPath, new UTF8Encoding(false))
    {
    }

    public async Task WriteOutput(string path, string output, CancellationToken cancellationToken)
    {
        var outputPath = Path.Combine(rootDirectoryPath, path);
        await File.WriteAllTextAsync(outputPath, output, encoding, cancellationToken);
    }
}
