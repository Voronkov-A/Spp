using System.Text.RegularExpressions;
using Spp.Authorization.Domain.Common.Exceptions;

namespace Spp.Authorization.Domain.Rbac;

public readonly struct Translation
{
    private static readonly Regex LanguageRegex = new("[a-z]{2}-[A-Z]{2}");

    public Translation(string language, string value)
    {
        if (!LanguageRegex.IsMatch(language))
        {
            throw new InvalidNameException("Invalid language format.", language);
        }

        if (value.Length > 256)
        {
            throw new InvalidNameException("Value is too long.", value);
        }

        Language = language;
        Value = value;
    }

    public string Language { get; }

    public string Value { get; }
}
