using Duende.IdentityServer.Stores;
using Microsoft.Extensions.DependencyInjection;
using Spp.Common.Configuration;
using Spp.Common.Postgres;
using Spp.Common.Postgres.EntityFramework;
using Spp.IdentityProvider.Persistence.Authorization;

namespace Spp.IdentityProvider.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services
            .AddSettings<ConnectionSettings>("Persistence:Connection")
            .AddPostgresDbContext<AuthorizationDbContext>(AuthorizationDbContext.SchemaName)
            .AddTransient<IClientStore, ClientStore>();
    }
}
