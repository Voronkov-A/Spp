using Microsoft.Extensions.Localization;
using OrchardCore.Localization;
using System.Globalization;

namespace Spp.Common.Localization;

public class PluralFallbackStringLocalizer(IPluralStringLocalizer inner, CultureInfo fallbackCulture) :
    FallbackStringLocalizer(inner, fallbackCulture), IPluralStringLocalizer
{
    private readonly CultureInfo _fallbackCulture = fallbackCulture;

    public (LocalizedString, object[]) GetTranslation(string name, params object[] arguments)
    {
        var result = inner.GetTranslation(name, arguments);

        if (result.Item1.ResourceNotFound)
        {
            using var fallback = new CultureInfoSubstitution(_fallbackCulture);
            result = inner.GetTranslation(name, arguments);
        }

        return result;
    }
}
