using Spp.Common.Authentication.Abstractions;

namespace Spp.Authorization.Client.Sdk;

internal class AuthorizationAuthenticationContext :
    AuthenticationContext,
    IAuthenticationContext<AuthorizationAuthenticationContext>;
