using System.Collections.Generic;

namespace Spp.IdentityProvider.TestServer;

public class DefaultUserSetSettings
{
    public required IReadOnlyCollection<DefaultUserSettings> Users { get; init; }
}
