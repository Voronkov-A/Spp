using Npgsql;

namespace Spp.Common.Postgres;

public static class ConnectionSettingsExtensions
{
    public static string CreateConnectionString(this ConnectionSettings self)
    {
        return new NpgsqlConnectionStringBuilder(self.Options ?? "")
        {
            Host = self.Hostname,
            Port = self.Port,
            Username = self.Username,
            Password = self.Password,
            Database = self.Database
        }.ToString();
    }

    public static string CreateMasterConnectionString(this ConnectionSettings self)
    {
        return new NpgsqlConnectionStringBuilder(self.Options ?? "")
        {
            Host = self.Hostname,
            Port = self.Port,
            Username = self.Username,
            Password = self.Password,
            Database = "postgres"
        }.ToString();
    }
}
