using Microsoft.Extensions.DependencyInjection;
using Spp.Common.Initialization;

namespace Spp.IdentityProvider.Initialization;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInitialization(this IServiceCollection services)
    {
        return services
            .AddInitializer<PersistenceInitializer>()
            .AddInitializer<DefaultApplicationInitializer>()
            .AddInitializer<DefaultUserInitializer>();
    }
}
