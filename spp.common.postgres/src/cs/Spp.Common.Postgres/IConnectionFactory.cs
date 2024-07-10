using System.Data.Common;

namespace Spp.Common.Postgres;

public interface IConnectionFactory
{
    DbConnection CreateConnection(string connectionString);
}
