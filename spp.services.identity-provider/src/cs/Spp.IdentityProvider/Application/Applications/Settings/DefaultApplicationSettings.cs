using System;
using System.Collections.Generic;

namespace Spp.IdentityProvider.Application.Applications.Settings;

public class DefaultApplicationSettings
{
    public required string ClientId { get; init; }

    public required string ClientSecret { get; init; }

    public required IReadOnlyCollection<Uri> RedirectUris { get; init; }
}
