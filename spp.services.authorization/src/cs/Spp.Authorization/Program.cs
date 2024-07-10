using Spp.Common.Hosting;
using System.Threading.Tasks;

namespace Spp.Authorization;

internal static class Program
{
    private static async Task Main(string[] _)
    {
        await DefaultHost.CreateBuilder()
            .WithDefaultConfiguration()
            .WithDefaultLogging()
            .WithServices(AuthorizationStartup.ConfigureServices)
            .Build()
            .WithMiddlewares(AuthorizationStartup.Configure)
            .RunAsync();
    }
}
