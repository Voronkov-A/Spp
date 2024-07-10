using Microsoft.AspNetCore.Mvc;
using Spp.IdentityProvider.WebApi.Errors.V1;

namespace Spp.IdentityProvider.WebApi.Errors;

public interface IIdentityProviderErrorFactory
{
    ProblemDetails DuplicateClientId(string detail, DuplicateClientIdErrorParameters parameters);

    ProblemDetails DuplicateUsername(string detail, DuplicateUsernameErrorParameters parameters);
}
