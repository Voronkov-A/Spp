using Microsoft.Extensions.DependencyInjection;
using System;

namespace Spp.Common.Initialization;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInitializer<T>(this IServiceCollection services) where T : class, IInitializer
    {
        return services
            .AddScoped<T>()
            .AddHostedService<InitializationHostedService<T>>();
    }

    public static IServiceCollection AddInitializer<T>(
        this IServiceCollection services,
        Func<IServiceProvider, T> factory)
        where T : class, IInitializer
    {
        return services
            .AddScoped(factory)
            .AddHostedService<InitializationHostedService<T>>();
    }
}
