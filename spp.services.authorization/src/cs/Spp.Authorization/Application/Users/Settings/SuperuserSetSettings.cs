using System.Collections.Generic;

namespace Spp.Authorization.Application.Users.Settings;

public class SuperuserSetSettings
{
    public required IReadOnlyCollection<SuperuserSettings> Items { get; init; }
}
