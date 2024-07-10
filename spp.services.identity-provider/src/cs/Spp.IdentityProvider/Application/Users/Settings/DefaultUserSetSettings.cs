using System.Collections.Generic;

namespace Spp.IdentityProvider.Application.Users.Settings;

public class DefaultUserSetSettings
{
    public required IReadOnlyCollection<DefaultUserSettings> Users { get; init; }
}
