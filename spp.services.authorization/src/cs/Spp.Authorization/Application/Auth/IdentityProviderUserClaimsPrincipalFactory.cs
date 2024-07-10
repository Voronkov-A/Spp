using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Duende.IdentityServer;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Spp.Authorization.Client.Sdk.Domain;
using Spp.Authorization.Domain.Users;
using Spp.Common.Authorization;

namespace Spp.Authorization.Application.Auth;

public class IdentityProviderUserClaimsPrincipalFactory(IOptions<IdentityOptions> optionsAccessor) :
    IUserClaimsPrincipalFactory<User>
{
    public Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var options = optionsAccessor.Value;
        var claims = new Claim[]
        {
            new(options.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
            new(options.ClaimsIdentity.UserNameClaimType, user.Name.ToString()),
            new(JwtClaimTypes.AuthenticationTime, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
            new(JwtClaimTypes.IdentityProvider, IdentityServerConstants.LocalIdentityProvider),
            new(JwtClaimTypes.PreferredUserName, user.Name.ToString()),
            new(SuperuserClaim.Type, user.IsSuperuser.ToString())
        }.Concat(user.Roles.Select(x => new Claim(RoleClaim.Type, x.ToString())));

        var identity = new ClaimsIdentity(
            claims,
            "Identity.Application",
            options.ClaimsIdentity.UserNameClaimType,
            options.ClaimsIdentity.RoleClaimType);
        var principal = new ClaimsPrincipal(identity);
        return Task.FromResult(principal);
    }
}
