using System;

namespace Spp.Authorization.Tests.TestHelpers.Services;

public class AuthorizationContract(Uri webApiUrl, AuthorizationConfiguration configuration)
{
    public Uri WebApiUrl { get; } = webApiUrl;

    public AuthorizationConfiguration Configuration { get; } = configuration;
}
