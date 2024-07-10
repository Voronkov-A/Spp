using Spp.Common.Authentication.Abstractions;

namespace Spp.Authorization.Application;

public class ApplicationRegistrationAuthenticationContext :
    AuthenticationContext,
    IAuthenticationContext<ApplicationRegistrationAuthenticationContext>;
