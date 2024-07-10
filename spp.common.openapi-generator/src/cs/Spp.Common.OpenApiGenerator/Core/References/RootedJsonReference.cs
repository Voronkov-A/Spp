using System;

namespace Spp.Common.OpenApiGenerator.Core.References;

public class RootedJsonReference
{
    private readonly JsonReference _inner;

    public RootedJsonReference(JsonReference reference)
    {
        if (reference.DocumentPath == null)
        {
            throw new ArgumentException("Reference must have a document path defined.", nameof(reference));
        }

        _inner = reference;
    }

    public RootedJsonReference(DocumentPath documentPath, JsonPointer? tokenPath = null)
    {
        _inner = new JsonReference(documentPath, tokenPath);
    }

    public DocumentPath DocumentPath => _inner.DocumentPath!.Value;

    public JsonPointer? TokenPath => _inner.TokenPath;

    public static RootedJsonReference Parse(string reference)
    {
        return new RootedJsonReference(JsonReference.Parse(reference));
    }
}
