using Microsoft.AspNetCore.Authentication;

namespace Spp.Common.Authentication.TokenAuthenticationScheme;

public class TokenAuthenticationOptions : AuthenticationSchemeOptions
{
    public new TokenAuthenticationEvents Events
    {
        get => (TokenAuthenticationEvents)base.Events!;
        set => base.Events = value;
    }
}
