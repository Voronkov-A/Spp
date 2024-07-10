using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Spp.Common.Postgres.EntityFramework;

public abstract class BaseDesignTimeDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
    where TContext : DbContext
{
    public TContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<TContext>().UseNpgsql("Server=127.0.0.1").Options;
        return CreateDbContext(options);
    }

    protected abstract TContext CreateDbContext(DbContextOptions<TContext> options);
}
