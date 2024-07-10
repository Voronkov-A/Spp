namespace Spp.Common.OpenApiGenerator.Core.References;

// https://niem.github.io/json/reference/json-schema/references/
public readonly struct JsonReference(DocumentPath? documentPath, JsonPointer? tokenPath)
{
    public DocumentPath? DocumentPath { get; } = documentPath;

    public JsonPointer? TokenPath { get; } = tokenPath;

    public override string ToString()
    {
        return (DocumentPath, TokenPath) switch
        {
            (null, _) => $"#{TokenPath}",
            (_, null) => DocumentPath.Value.ToString(),
            _ => $"{DocumentPath}#{TokenPath}"
        };
    }

    public static JsonReference Parse(string reference)
    {
        var separatorIndex = reference.LastIndexOf('#');

        var documentPath = separatorIndex switch
        {
            < 0 => reference,
            0 => null,
            _ => reference[0..separatorIndex]
        };

        var tokenPath = separatorIndex < 0 || separatorIndex == reference.Length - 1
            ? null
            : reference[(separatorIndex + 1)..];

        if (tokenPath is "" or "/")
        {
            tokenPath = null;
        }

        return new JsonReference(
            documentPath == null ? null : References.DocumentPath.Parse(documentPath),
            tokenPath == null ? null : JsonPointer.Parse(tokenPath));
    }
}
