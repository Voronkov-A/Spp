using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Spp.Common.Initialization;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInitializer<T>(this IServiceCollection services) where T : class, IInitializer
    {
        if (!services.Any(x => x.ImplementationType == typeof(InitializationHostedService)))
        {
            services.AddHostedService<InitializationHostedService>();
        }

        return services.AddScoped<IInitializer, T>();
    }
}
