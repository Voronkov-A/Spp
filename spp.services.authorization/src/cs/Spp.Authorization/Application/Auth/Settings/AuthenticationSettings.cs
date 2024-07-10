using System;
using System.Collections.Generic;

namespace Spp.Authorization.Application.Auth.Settings;

public class AuthenticationSettings
{
    public required string ClientId { get; init; }

    public required string ClientSecret { get; init; }

    public required IReadOnlyCollection<Uri> RedirectUris { get; init; }

    public required string Audience { get; init; }

    public required IReadOnlyCollection<string> Issuers { get; init; }

    public required string KeyPath { get; init; }
}
