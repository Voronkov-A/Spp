using Spp.Common.Authentication.Oidc;

namespace Spp.Authorization.Client.Sdk.Settings;

internal class AuthorizationServiceSettings :
    ClientCredentialsAccessTokenAcquirerSettings<AuthorizationAuthenticationContext>
{
    public required string Url { get; init; }
}
