using Microsoft.EntityFrameworkCore;
using Spp.Common.Postgres.EntityFramework;

namespace Spp.IdentityProvider.Persistence.Authorization;

public class AuthorizationDesignTimeDbContextFactory : BaseDesignTimeDbContextFactory<AuthorizationDbContext>
{
    protected override AuthorizationDbContext CreateDbContext(DbContextOptions<AuthorizationDbContext> options)
    {
        return new AuthorizationDbContext(options);
    }
}
