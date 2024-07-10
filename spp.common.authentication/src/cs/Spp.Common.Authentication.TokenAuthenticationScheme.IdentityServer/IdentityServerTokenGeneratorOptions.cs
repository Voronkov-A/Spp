using IdentityModel;

namespace Spp.Common.Authentication.TokenAuthenticationScheme.IdentityServer;

public class IdentityServerTokenGeneratorOptions
{
    public string ClientId { get; set; } = "client";

    public string ClientSecret { get; set; } = "secret";

    public string ScopePropertyKey { get; set; } = "scope";

    public string DefaultScope { get; set; }
        = $"{OidcConstants.StandardScopes.OpenId} {OidcConstants.StandardScopes.Profile}";
}
