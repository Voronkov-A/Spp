using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.OpenApiGenerator.Core;
using Spp.Common.OpenApiGenerator.Core.References;
using Spp.Common.OpenApiGenerator.Serialization;

namespace Spp.Common.OpenApiGenerator.FileSystem;

public class FileDocumentResolver(string rootDirectoryPath, IOpenApiDocumentSerializer serializer) :
    IDocumentResolver
{
    public FileDocumentResolver(string rootDirectoryPath) :
        this(rootDirectoryPath, new YamlOpenApiDocumentSerializer())
    {
    }

    public async Task<GenerationContext> ResolveDocument(DocumentPath path, CancellationToken cancellationToken)
    {
        var inputPath = Path.Combine(rootDirectoryPath, path.ToString());
        var content = await File.ReadAllBytesAsync(inputPath, cancellationToken);
        var deserializedContent = serializer.Deserialize(content);
        return new GenerationContext(new RootedJsonReference(path), deserializedContent);
    }
}
