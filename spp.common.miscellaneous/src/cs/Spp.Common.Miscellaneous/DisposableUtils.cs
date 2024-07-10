using System;
using System.Threading.Tasks;

namespace Spp.Common.Miscellaneous;

public static class DisposableUtils
{
    public static async ValueTask Dispose(IAsyncDisposable? disposable)
    {
        if (disposable != null)
        {
            await disposable.DisposeAsync();
        }
    }
}
