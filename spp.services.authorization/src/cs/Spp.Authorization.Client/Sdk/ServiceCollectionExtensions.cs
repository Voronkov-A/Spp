using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Spp.Authorization.Client.Auth.V1;
using Spp.Authorization.Client.Sdk.Background;
using Spp.Authorization.Client.Sdk.Domain;
using Spp.Authorization.Client.Sdk.Initialization;
using Spp.Authorization.Client.Sdk.Persistence;
using Spp.Authorization.Client.Sdk.Persistence.Schema;
using Spp.Authorization.Client.Sdk.Persistence.Schema.Migrations;
using Spp.Authorization.Client.Sdk.Settings;
using Spp.Authorization.Events.Rbac;
using Spp.Common.Authentication.Abstractions;
using Spp.Common.Authentication.Http;
using Spp.Common.Authentication.Oidc;
using Spp.Common.Authorization;
using Spp.Common.Configuration;
using Spp.Common.Initialization;
using Spp.Common.Mediator;
using Spp.Common.Migrations;
using Spp.Common.Migrations.Postgres;
using Spp.Common.Miscellaneous.DependencyInjection;
using Spp.Common.Subscriptions.InMemory;
using Spp.Common.Transactions;
using System;

namespace Spp.Authorization.Client.Sdk;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDefaultAuthorization(
        this IServiceCollection services,
        string serviceId,
        Action<IAuthorizationConfigurator> configure)
    {
        var configurator = new AuthorizationConfigurator(serviceId);
        configure(configurator);

        services.Configure<AuthorizationPersistenceOptions>(
            options => options.SchemaName = configurator.SchemaName);
        services.Configure<PostgresMigrationStoreOptions<AuthorizationDatabase>>(
            options => options.SchemaName = configurator.SchemaName);
        services
            .AddHttpClient<IRbacClient, RbacClient>(
                (sp, c) =>
                    c.BaseAddress = new Uri(sp.GetRequiredService<IOptions<AuthorizationServiceSettings>>().Value.Url))
            .AddHttpMessageHandler<BearerAuthenticationHttpMessageHandler<AuthorizationAuthenticationContext>>();
        services
            .AddHttpClient<
                IAccessTokenAcquirer<AuthorizationAuthenticationContext>,
                ClientCredentialsAccessTokenAcquirer<AuthorizationAuthenticationContext>>(
                (sp, c) =>
                    c.BaseAddress = new Uri(sp.GetRequiredService<IOptions<AuthorizationServiceSettings>>().Value.Url));

        return services
            .AddTransient<BearerAuthenticationHttpMessageHandler<AuthorizationAuthenticationContext>>()
            .AddSingleton<AccessTokenCache>()
            .AddSingleton<
                IAuthenticationContext<AuthorizationAuthenticationContext>,
                AuthorizationAuthenticationContext>()
            .AddSettings<ClientCredentialsAccessTokenAcquirerSettings<AuthorizationAuthenticationContext>>(
                configurator.AuthorizationServiceSettingsSection)

            .AddAuthorization(configurator.Configure)
            .AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>()
            .AddSingleton(configurator.PermissionGroup)
            .AddSingleton<IPermissionResolver, PermissionResolver>()
            .AddDecorator<IPermissionResolver, EdgeCasePermissionResolver>()
            .AddHostedService<RegisterPermissionGroupBackgroundService>()

            .AddScoped<IMigration<AuthorizationDatabase>, _0001_Initialize>()

            .AddInitializer<AuthorizationPersistenceInitializer>()
            .AddScoped<IAuthorizationMigrator, AuthorizationMigrator>()
            .AddScoped<IMigrationStore<AuthorizationDatabase>, PostgresMigrationStore<AuthorizationDatabase>>()
            .AddSingleton<AuthorizationDatabase>()
            .AddSingleton<AuthorizationPersistenceEventSubscription>()
            .AddSettings<AuthorizationServiceSettings>(configurator.AuthorizationServiceSettingsSection)
            .AddSettings<AuthorizationPersistenceSettings>(configurator.PersistenceSettingsSection)
            .AddSingleton<IRoleStore, PostgresRoleStore>()

            .AddHandler<UpdateAuthorizationPersistenceCommand<RoleCreated>, PostgresRoleStoreEventHandler>()
            .AddHandler<UpdateAuthorizationPersistenceCommand<RoleDeleted>, PostgresRoleStoreEventHandler>()

            .AddScoped<
                IUnitOfWorkFilter,
                InMemorySubscriptionUnitOfWorkFilter<AuthorizationPersistenceEventSubscription>>();
    }
}
