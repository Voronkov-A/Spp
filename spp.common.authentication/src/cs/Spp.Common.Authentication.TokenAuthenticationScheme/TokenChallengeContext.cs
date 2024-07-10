using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Spp.Common.Authentication.TokenAuthenticationScheme;

public class TokenChallengeContext(
    HttpContext context,
    AuthenticationScheme scheme,
    TokenAuthenticationOptions options,
    AuthenticationProperties? properties) :
    PropertiesContext<TokenAuthenticationOptions>(context, scheme, options, properties)
{
    public bool Handled { get; set; }
}
