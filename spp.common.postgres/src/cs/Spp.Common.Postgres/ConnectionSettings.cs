namespace Spp.Common.Postgres;

public class ConnectionSettings
{
    public required string Hostname { get; init; }

    public required int Port { get; init; }

    public required string Username { get; init; }

    public required string Password { get; init; }

    public required string Database { get; init; }

    public string? Options { get; init; }
}
