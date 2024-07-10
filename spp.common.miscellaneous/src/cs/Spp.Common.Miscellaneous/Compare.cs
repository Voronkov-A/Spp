using System;
using System.Collections.Generic;

namespace Spp.Common.Miscellaneous;

public static class Compare<T>
{
    public static IComparer<T> By<TKey>(Func<T, TKey> keyAccessor)
    {
        return new ByKeyComparer<T, TKey>(keyAccessor, Comparer<TKey>.Default);
    }
}
