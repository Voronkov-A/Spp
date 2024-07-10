using Microsoft.Extensions.Localization;
using OrchardCore.Localization;
using System;
using System.Globalization;

namespace Spp.Common.Localization;

public class FallbackStringLocalizerFactory(IStringLocalizerFactory inner, CultureInfo fallbackCulture) :
    IStringLocalizerFactory
{
    public IStringLocalizer Create(Type resourceSource)
    {
        var localizer = inner.Create(resourceSource);
        return localizer is IPluralStringLocalizer pluralLocalizer
            ? new PluralFallbackStringLocalizer(pluralLocalizer, fallbackCulture)
            : new FallbackStringLocalizer(localizer, fallbackCulture);
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        var localizer = inner.Create(baseName, location);
        return localizer is IPluralStringLocalizer pluralLocalizer
            ? new PluralFallbackStringLocalizer(pluralLocalizer, fallbackCulture)
            : new FallbackStringLocalizer(localizer, fallbackCulture);
    }
}
