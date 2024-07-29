using Microsoft.Extensions.DependencyInjection;
using Spp.Common.Configuration;

namespace Spp.Common.Synchronization.Postgres;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgresTableDistributedLocks(
        this IServiceCollection services,
        string connectionSettingsSectionName = "Persistence:Connection",
        string schemaName = "synchronization")
    {
        return services
            .AddSettings<PostgresTableDistributedLockSettings>(connectionSettingsSectionName)
            .Configure<PostgresTableDistributedLockOptions>(opt => opt.SchemaName = schemaName)
            .AddSingleton<SynchronizationDatabase>()
            .AddSingleton<IDistributedLockFactory, PostgresTableDistributedLockFactory>();
    }
}
