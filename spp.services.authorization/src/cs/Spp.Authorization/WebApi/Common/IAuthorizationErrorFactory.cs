using Microsoft.AspNetCore.Mvc;
using Spp.Authorization.WebApi.Common.V1;

namespace Spp.Authorization.WebApi.Common;

public interface IAuthorizationErrorFactory
{
    ProblemDetails InvalidName(string detail, InvalidNameErrorParameters parameters);

    ProblemDetails PermissionNotFound(string detail, PermissionNotFoundErrorParameters parameters);
}
