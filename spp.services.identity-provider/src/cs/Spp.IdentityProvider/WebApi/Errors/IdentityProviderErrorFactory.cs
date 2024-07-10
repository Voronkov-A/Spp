using Microsoft.AspNetCore.Mvc;
using Spp.IdentityProvider.WebApi.Errors.V1;
using System.Net;
using Spp.Common.Errors;

namespace Spp.IdentityProvider.WebApi.Errors;

public class IdentityProviderErrorFactory(IErrorFactory inner) : IIdentityProviderErrorFactory
{
    public ProblemDetails DuplicateClientId(string detail, DuplicateClientIdErrorParameters parameters)
    {
        return inner.CreateError(
            IdentityProviderErrorCode.IdentityProviderDuplicateClientId,
            HttpStatusCode.BadRequest,
            "Duplicate client identifier",
            detail,
            parameters);
    }

    public ProblemDetails DuplicateUsername(string detail, DuplicateUsernameErrorParameters parameters)
    {
        return inner.CreateError(
            IdentityProviderErrorCode.IdentityProviderDuplicateUsername,
            HttpStatusCode.BadRequest,
            "Duplicate username",
            detail,
            parameters);
    }
}
