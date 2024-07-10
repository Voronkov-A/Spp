namespace Spp.Common.Migrations.Postgres;

public class PostgresMigrationStoreOptions
{
    public string SchemaName { get; set; } = "public";
}

public class PostgresMigrationStoreOptions<T> : PostgresMigrationStoreOptions;
