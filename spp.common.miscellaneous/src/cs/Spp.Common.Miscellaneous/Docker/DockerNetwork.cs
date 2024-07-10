using System;

namespace Spp.Common.Miscellaneous.Docker;

public static class DockerNetwork
{
    public static string OverwriteHostname(string hostname)
    {
        return hostname.Equals("localhost", StringComparison.OrdinalIgnoreCase) || hostname == "127.0.0.1"
            ? "172.17.0.1"
            : hostname;
    }
}
