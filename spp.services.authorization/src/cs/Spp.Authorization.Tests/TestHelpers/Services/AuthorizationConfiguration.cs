using Spp.Authorization.Application.Auth.Settings;
using Spp.Authorization.Application.Users.Settings;

namespace Spp.Authorization.Tests.TestHelpers.Services;

public class AuthorizationConfiguration
{
    public required AuthenticationSettings Authentication { get; init; }

    public required SuperuserSetSettings SuperuserSet { get; init; }
}
