using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Spp.Common.Localization.AspNetCore;

internal class SupportedCultureCollection : IEnumerable<CultureInfo>
{
    public static CultureInfo DefaultCulture { get; } = new("en-US");

    private readonly CultureInfo[] _items;

    public SupportedCultureCollection()
    {
        _items = new[]
        {
            DefaultCulture,
            new("ru-RU")
        };
    }

    public IEnumerator<CultureInfo> GetEnumerator()
    {
        return _items.AsEnumerable().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _items.GetEnumerator();
    }
}
