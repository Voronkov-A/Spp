using Microsoft.Extensions.DependencyInjection;
using Spp.Authorization.Domain.GeneratedUserNameParts.Repositories;
using Spp.Authorization.Domain.Rbac;
using Spp.Authorization.Domain.Rbac.Repositories;
using Spp.Authorization.Domain.Users.Repositories;
using Spp.Authorization.Events.GeneratedUserNameParts;
using Spp.Authorization.Events.Rbac;
using Spp.Authorization.Events.Users;
using Spp.Authorization.Persistence.GeneratedUserNameParts;
using Spp.Authorization.Persistence.Rbac;
using Spp.Authorization.Persistence.Schemas.Indices;
using Spp.Authorization.Persistence.Users;
using Spp.Common.Cqs;
using Spp.Common.EventSourcing;
using Spp.Common.EventSourcing.EventStore.Postgres;
using Spp.Common.Mediator;
using Spp.Common.Migrations;
using Spp.Common.Migrations.Postgres;
using Spp.Common.Miscellaneous;
using Spp.Common.Subscriptions.InMemory;
using Spp.Common.Transactions;

namespace Spp.Authorization.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.Configure<PostgresMigrationStoreOptions<IndicesDatabase>>(options => options.SchemaName = "indices");

        return services
            .AddScoped<IIndicesMigrator, IndicesMigrator>()
            .AddSingleton<IndicesDatabase>()
            .AddScoped<IMigrationStore<IndicesDatabase>, PostgresMigrationStore<IndicesDatabase>>()
            .AddScoped<IMigration<IndicesDatabase>, Schemas.Indices.Migrations._0001_Initialize>()

            .AddPostgresJsonEventStore(
                new AggregateTypeConfigurationBuilder()
                    .WithAggregatesFrom(typeof(PermissionGroup).Assembly)
                    .WithEventsFrom(typeof(PermissionGroupCreated).Assembly)
                    .Build(),
                "Persistence:Connection")
            .AddSingleton<IndexEventSubscription>()
            .AddScoped<IUnitOfWorkFilter, InMemorySubscriptionUnitOfWorkFilter<IndexEventSubscription>>()

            .AddScoped<IGeneratedUserNamePartRepository, GeneratedUserNamePartRepository>()
            .AddScoped<IGeneratedUserNamePartIndex, GeneratedUserNamePartIndex>()
            .AddHandler<
                UpdateIndexCommand<GeneratedUserNamePartCreated>,
                GeneratedUserNamePartIndexEventHandler>()
            .AddHandler<
                UpdateIndexCommand<GeneratedUserNamePartDeleted>,
                GeneratedUserNamePartIndexEventHandler>()

            .AddScoped<IPermissionGroupRepository, PermissionGroupRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IRoleIndex, RoleIndex>()
            .AddHandler<UpdateIndexCommand<RoleCreated>, Unit, RoleIndexEventHandler>()
            .AddHandler<UpdateIndexCommand<RoleDeleted>, Unit, RoleIndexEventHandler>()

            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserIndex, UserIndex>()
            .AddHandler<UpdateIndexCommand<UserCreated>, Unit, UserIndexEventHandler>()
            .AddHandler<UpdateIndexCommand<UserRenamed>, Unit, UserIndexEventHandler>();
    }
}
