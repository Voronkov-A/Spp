using System.Collections.Generic;

namespace Spp.Authorization.Application.Users.Settings;

public class SuperuserSettings
{
    public required IReadOnlyCollection<SuperuserIdentitySettings> Identities { get; init; }
}
