using Microsoft.AspNetCore.Mvc;

namespace Spp.Common.Errors;

public interface ICommonErrorFactory
{
    ProblemDetails AuthenticationFailure(string detail);

    ProblemDetails AuthorizationFailure(string detail);

    ProblemDetails ResourceNotFound(string detail);
}
