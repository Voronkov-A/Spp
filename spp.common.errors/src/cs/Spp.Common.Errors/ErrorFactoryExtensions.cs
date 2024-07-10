using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Spp.Common.Errors;

public static class ErrorFactoryExtensions
{
    public static ProblemDetails CreateError<TErrorCode>(
            this IErrorFactory self,
            TErrorCode code,
            HttpStatusCode status,
            string title,
            string detail,
            object? parameters = null)
            where TErrorCode : struct, Enum
    {
        return self.CreateError(code, (int)status, title, detail, parameters);
    }
}
