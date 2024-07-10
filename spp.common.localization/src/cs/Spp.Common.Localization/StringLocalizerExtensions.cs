using Microsoft.Extensions.Localization;
using Spp.Common.Miscellaneous;
using System;

namespace Spp.Common.Localization;

public static class StringLocalizerExtensions
{
    public static string Get(this IStringLocalizer localizer, string key, params string[] placeholders)
    {
        return localizer[key, placeholders].Value;
    }

    public static string Get<TKey>(this IStringLocalizer<TKey> localizer, TKey key, params string[] placeholders)
        where TKey : struct, Enum
    {
        return Get(localizer, EnumSerializer.ToString(key), placeholders);
    }
}
