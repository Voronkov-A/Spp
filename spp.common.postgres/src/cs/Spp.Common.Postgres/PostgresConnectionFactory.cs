using Npgsql;
using System.Data.Common;

namespace Spp.Common.Postgres;

public class PostgresConnectionFactory : IConnectionFactory
{
    public DbConnection CreateConnection(string connectionString)
    {
        return new NpgsqlConnection(connectionString);
    }
}
