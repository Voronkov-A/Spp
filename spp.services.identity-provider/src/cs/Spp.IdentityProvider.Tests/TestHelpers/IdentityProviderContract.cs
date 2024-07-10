using Spp.IdentityProvider.TestServer;
using System;

namespace Spp.IdentityProvider.Tests.TestHelpers;

public class IdentityProviderContract(Uri webApiUrl, IdentityProviderConfiguration configuration)
{
    public Uri WebApiUrl { get; } = webApiUrl;

    public IdentityProviderConfiguration Configuration { get; } = configuration;
}
