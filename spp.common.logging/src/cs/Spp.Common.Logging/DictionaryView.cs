using System.Collections.Generic;
using System.Text.Json;

namespace Spp.Common.Logging;

internal class DictionaryView<TKey, TValue> : Dictionary<TKey, TValue> where TKey : notnull
{
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
