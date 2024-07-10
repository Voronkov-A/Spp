using Microsoft.Extensions.DependencyInjection;
using Spp.Common.Initialization;

namespace Spp.Authorization.Initialization;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInitialization(this IServiceCollection services)
    {
        return services
            .AddInitializer<ApplicationRegistrationInitializer>()
            .AddInitializer<EventStoreInitializer>()
            .AddInitializer<SuperuserInitializer>();
    }
}
