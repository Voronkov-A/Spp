using Spp.IdentityProvider.Application.Applications.Settings;
using Spp.IdentityProvider.Application.Users.Settings;

namespace Spp.IdentityProvider.Tests.TestHelpers;

public class IdentityProviderConfiguration
{
    public required DefaultApplicationSettings DefaultApplication { get; init; }

    public required DefaultUserSetSettings DefaultUserSet { get; init; }
}
