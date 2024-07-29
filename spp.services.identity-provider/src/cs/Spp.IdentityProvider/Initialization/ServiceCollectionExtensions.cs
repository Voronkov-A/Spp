using Microsoft.Extensions.DependencyInjection;
using Spp.Common.Initialization;
using Spp.Common.Miscellaneous.DependencyInjection;
using Spp.Common.Synchronization.Initialization;
using Spp.Common.Synchronization.Postgres;

namespace Spp.IdentityProvider.Initialization;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInitialization(this IServiceCollection services)
    {
        return services
            .AddInitializer<PostgresTableDistributedLockInitializer>()
            .AddInitializer<PersistenceInitializer>()
            .AddDecorator<IInitializer, CriticalSectionInitializer>()
            .AddInitializer<DefaultApplicationInitializer>()
            .AddDecorator<IInitializer, CriticalSectionInitializer>()
            .AddInitializer<DefaultUserInitializer>()
            .AddDecorator<IInitializer, CriticalSectionInitializer>();
    }
}
