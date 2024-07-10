using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Spp.Common.Localization;

public class FallbackStringLocalizer(IStringLocalizer inner, CultureInfo fallbackCulture) : IStringLocalizer
{
    public LocalizedString this[string name]
    {
        get
        {
            var result = inner[name];

            if (result.ResourceNotFound)
            {
                using var fallback = new CultureInfoSubstitution(fallbackCulture);
                result = inner[name];
            }

            return result;
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var result = inner[name, arguments];

            if (result.ResourceNotFound)
            {
                using var fallback = new CultureInfoSubstitution(fallbackCulture);
                result = inner[name, arguments];
            }

            return result;
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        return inner.GetAllStrings(includeParentCultures);
    }

    protected readonly struct CultureInfoSubstitution : IDisposable
    {
        private readonly CultureInfo _ambientCultureInfo;

        public CultureInfoSubstitution(CultureInfo cultureInfo)
        {
            _ambientCultureInfo = CultureInfo.CurrentUICulture;
            CultureInfo.CurrentUICulture = cultureInfo;
        }

        public void Dispose()
        {
            CultureInfo.CurrentUICulture = _ambientCultureInfo;
        }
    }
}
