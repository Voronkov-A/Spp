using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Spp.Common.Postgres.EntityFramework.Converters;
using Spp.IdentityProvider.Domain.Users;
using Spp.IdentityProvider.Persistence.Authorization.Configurations;

namespace Spp.IdentityProvider.Persistence.Authorization;

public class AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options)
    : ApiAuthorizationDbContext<User>(
        options,
        new OptionsWrapper<OperationalStoreOptions>(new OperationalStoreOptions()))
{
    public const string SchemaName = "authorization";

    public DbSet<Domain.Applications.Application> Applications { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(SchemaName);
        builder.ApplyConfiguration(new ApplicationConfiguration());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder
            .Properties<Domain.Applications.ApplicationId>()
            .HaveConversion<StringValueConverter<Domain.Applications.ApplicationId>>();
    }
}
