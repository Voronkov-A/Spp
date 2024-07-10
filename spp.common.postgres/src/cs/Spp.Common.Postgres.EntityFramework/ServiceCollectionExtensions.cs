using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Spp.Common.Postgres.EntityFramework;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgresDbContext<TContext>(
        this IServiceCollection services,
        string schemaName = "public")
        where TContext : DbContext
    {
        return services.AddDbContextPool<TContext>((sp, builder) =>
        {
            var connectionSettings = sp.GetRequiredService<IOptions<ConnectionSettings>>();
            builder.UseNpgsql(
                connectionSettings.Value.CreateConnectionString(),
                npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", schemaName));
        });
    }
}
