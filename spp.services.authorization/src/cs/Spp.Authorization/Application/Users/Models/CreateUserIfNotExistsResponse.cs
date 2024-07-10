using System.Security.Claims;

namespace Spp.Authorization.Application.Users.Models;

public class CreateUserIfNotExistsResponse(ClaimsPrincipal claimsPrincipal, bool isBlocked)
{
    public ClaimsPrincipal ClaimsPrincipal { get; } = claimsPrincipal;

    public bool IsBlocked { get; } = isBlocked;
}
