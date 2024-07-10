using System;
using System.Linq;
using System.Security.Claims;

namespace Spp.Common.Authorization;

internal static class ClaimsPrincipalExtensions
{
    public static bool IsClient(this ClaimsPrincipal user)
    {
        return !IsUser(user);
    }

    public static bool IsUser(this ClaimsPrincipal user)
    {
        return user.Claims.Any(x => x.Type == UserIdClaim.Type);
    }

    public static bool IsSuperuser(this ClaimsPrincipal user)
    {
        return user.Claims
            .Any(x =>
                x.Type == SuperuserClaim.Type
                && !"false".Equals(x.Value, StringComparison.InvariantCultureIgnoreCase));
    }
}
