using System;
using System.IO;

namespace Spp.Common.OpenApiGenerator.Core.References;

public readonly struct DocumentPath
{
    private readonly string _value = "";

    private DocumentPath(string value)
    {
        _value = value;
    }

    public DocumentPath ResolveFrom(DocumentPath source)
    {
        var uri = new Uri(_value, UriKind.RelativeOrAbsolute);

        if (uri.IsAbsoluteUri)
        {
            return new DocumentPath(_value);
        }

        var sourceDirectory = Path.GetDirectoryName(source._value);

        if (sourceDirectory == null)
        {
            return new DocumentPath(_value);
        }

        var sourceDirectoryUri = new Uri(sourceDirectory, UriKind.RelativeOrAbsolute);
        var targetUri = new Uri(sourceDirectoryUri, uri);
        return new DocumentPath(targetUri.ToString());
    }

    public override string ToString()
    {
        return _value;
    }

    public static DocumentPath Parse(string value)
    {
        return new DocumentPath(value);
    }
}
