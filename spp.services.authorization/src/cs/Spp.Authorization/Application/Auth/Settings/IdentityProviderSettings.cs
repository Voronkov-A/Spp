using System;
using System.Collections.Generic;

namespace Spp.Authorization.Application.Auth.Settings;

public class IdentityProviderSettings
{
    public required string Authority { get; init; }

    public required string ClientId { get; init; }

    public required string ClientSecret { get; init; }

    public required IReadOnlyCollection<Uri> RedirectUris { get; init; }

    public required string OldClientSecret { get; init; }

    public required Uri Url { get; init; }

    public required string Scope { get; init; }
}
