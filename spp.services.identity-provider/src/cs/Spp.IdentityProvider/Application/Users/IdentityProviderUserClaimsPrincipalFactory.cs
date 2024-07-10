using Duende.IdentityServer;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Spp.IdentityProvider.Domain.Users;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Spp.IdentityProvider.Application.Users;

public class IdentityProviderUserClaimsPrincipalFactory(
    UserManager<User> userManager,
    IOptions<IdentityOptions> optionsAccessor) :
    UserClaimsPrincipalFactory<User>(userManager, optionsAccessor)
{
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var claims = await base.GenerateClaimsAsync(user);
        claims.AddClaim(new Claim(
            JwtClaimTypes.AuthenticationTime,
            DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()));
        claims.AddClaim(new Claim(
            JwtClaimTypes.IdentityProvider,
            IdentityServerConstants.LocalIdentityProvider));

        if (user.UserName != null)
        {
            claims.AddClaim(new Claim(JwtClaimTypes.PreferredUserName, user.UserName));
        }

        return claims;
    }
}
