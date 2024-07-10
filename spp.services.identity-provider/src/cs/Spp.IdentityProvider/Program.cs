using System.Threading.Tasks;
using Spp.Common.Hosting;

namespace Spp.IdentityProvider;

public static class Program
{
    public static async Task Main(string[] args)
    {
        await DefaultHost.CreateBuilder()
            .WithDefaultConfiguration()
            .WithDefaultLogging()
            .WithServices(IdentityProviderStartup.ConfigureServices)
            .Build()
            .WithMiddlewares(IdentityProviderStartup.Configure)
            .RunAsync();
    }
}