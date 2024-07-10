#define DEBUG

namespace Spp.Common.Logging;

public partial class ScopedDebugLogger
{
    private static void DebugWriteLine(string message, string category)
    {
        System.Diagnostics.Debug.WriteLine(message, category);
    }
}
