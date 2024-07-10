using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Spp.Common.Configuration;
using Spp.Common.Postgres;
using Spp.Common.Transactions;

namespace Spp.Common.EventSourcing.EventStore.Postgres;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgresJsonEventStore(
        this IServiceCollection services,
        IAggregateTypeConfiguration configuration,
        string connectionSettingsSectionName = "Persistence:Connection",
        string schemaName = "persistence")
    {
        services.TryAddSingleton<IConnectionFactory, PostgresConnectionFactory>();
        return services
            .AddSettings<PostgresEventStoreConnectionSettings>(connectionSettingsSectionName)
            .Configure<PostgresEventStoreSchemaOptions>(opt => opt.SchemaName = schemaName)
            .AddSingleton<EventStoreDatabase>()
            .AddSingleton<IEventStore, PostgresEventStore>()
            .AddSingleton<IEventStoreMigrator, EventStoreMigrator>()
            .AddSingleton(configuration)
            .AddSingleton<IEventStoreRepository, EventStoreRepository>()
            .AddSingleton<IEventRepository>(sp => sp.GetRequiredService<IEventStoreRepository>())
            .AddSingleton<IEventSerializer, JsonEventSerializer>()
            .AddScoped<IUnitOfWorkFilter, EventUnitOfWorkFilter>();
    }
}
