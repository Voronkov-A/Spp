using System.Collections.Generic;

namespace Spp.IdentityProvider.WebApi.Auth;

public class AuthenticationSettings
{
    public required string Audience { get; init; }

    public required IReadOnlyCollection<string> Issuers { get; init; }

    public required string CrtPath { get; init; }

    public required string KeyPath { get; init; }
}
