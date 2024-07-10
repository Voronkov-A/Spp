using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Spp.Common.Authentication.TokenAuthenticationScheme;

public class TokenSigningInContext(
    HttpContext context,
    AuthenticationScheme scheme,
    TokenAuthenticationOptions options,
    AuthenticationProperties? properties,
    ClaimsPrincipal user) :
    PropertiesContext<TokenAuthenticationOptions>(context, scheme, options, properties)
{
    public ClaimsPrincipal User { get; set; } = user;

    public bool Handled { get; set; }
}
