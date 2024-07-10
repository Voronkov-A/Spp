using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Spp.Common.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSettings<T>(this IServiceCollection services, string key) where T : class
    {
        services
            .AddOptions<T>()
            .BindConfiguration(key)
            .Validate<IConfiguration>((value, configuration) =>
            {
                configuration.GetSettings<T>(key);
                return true;
            })
            .ValidateOnStart();
        return services;
    }
}
