using Microsoft.Extensions.DependencyInjection;

namespace Spp.Common.Transactions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        return services
            .AddScoped<IChangeTracker, ChangeTracker>()
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
