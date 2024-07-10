using System.Net;
using Microsoft.AspNetCore.Mvc;
using Spp.Authorization.WebApi.Common.V1;
using Spp.Common.Errors;

namespace Spp.Authorization.WebApi.Common;

public class AuthorizationErrorFactory(IErrorFactory errorFactory) : IAuthorizationErrorFactory
{
    public ProblemDetails InvalidName(string detail, InvalidNameErrorParameters parameters)
    {
        return errorFactory.CreateError(
            AuthorizationErrorCode.AuthorizationInvalidName,
            HttpStatusCode.BadRequest,
            "Invalid name",
            detail,
            parameters);
    }

    public ProblemDetails PermissionNotFound(string detail, PermissionNotFoundErrorParameters parameters)
    {
        return errorFactory.CreateError(
            AuthorizationErrorCode.AuthorizationPermissionNotFound,
            HttpStatusCode.BadRequest,
            "Permission not found",
            detail,
            parameters);
    }
}
