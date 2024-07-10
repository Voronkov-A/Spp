using System;
using System.Collections.Generic;

namespace Spp.Common.Miscellaneous;

public class ByKeyComparer<T, TKey>(Func<T, TKey> keyAccessor, IComparer<TKey> keyComparer) : IComparer<T>
{
    public int Compare(T? x, T? y)
    {
        return keyComparer.Compare(x is null ? default : keyAccessor(x), y is null ? default : keyAccessor(y));
    }
}
