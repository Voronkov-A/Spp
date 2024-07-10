using Microsoft.AspNetCore.Mvc;
using Spp.Common.Errors.V1;
using System.Net;

namespace Spp.Common.Errors;

internal class CommonErrorFactory(IErrorFactory inner) :
    ICommonErrorFactory
{
    public ProblemDetails AuthenticationFailure(string detail)
    {
        return inner.CreateError(
            CommonErrorCode.CommonAuthenticationFailure,
            HttpStatusCode.Unauthorized,
            "Authentication failure",
            detail);
    }

    public ProblemDetails AuthorizationFailure(string detail)
    {
        return inner.CreateError(
            CommonErrorCode.CommonAuthorizationFailure,
            HttpStatusCode.Forbidden,
            "Authorization failure",
            detail);
    }

    public ProblemDetails ResourceNotFound(string detail)
    {
        return inner.CreateError(
            CommonErrorCode.CommonResourceNotFound,
            HttpStatusCode.NotFound,
            "Resource not found",
            detail);
    }
}
