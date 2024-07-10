using Microsoft.AspNetCore.Authentication;

namespace Spp.IdentityProvider.WebApi.Auth;

public static class AuthenticationPropertiesExtensions
{
    public static void SetScope(this AuthenticationProperties properties, string scope)
    {
        properties.Items[AuthenticationPropertyKeys.Scope] = scope;
    }
}
