using System;

namespace Spp.IdentityProvider.TestServer;

public class IdentityProviderContract(Uri webApiUrl, IdentityProviderConfiguration configuration)
{
    public Uri WebApiUrl { get; } = webApiUrl;

    public IdentityProviderConfiguration Configuration { get; } = configuration;
}
