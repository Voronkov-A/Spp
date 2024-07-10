using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Spp.Common.Errors;

internal class ErrorFactory(IErrorUriProvider errorUriProvider) : IErrorFactory
{
    public ProblemDetails CreateError<TErrorCode>(
        TErrorCode code,
        int status,
        string title,
        string detail,
        object? parameters)
        where TErrorCode : struct, Enum
    {
        return new ProblemDetails
        {
            Type = errorUriProvider.GetTypeUri(code).ToString(),
            Status = status,
            Title = title,
            Detail = detail,
            Extensions = new Dictionary<string, object?>
            {
                ["parameters"] = parameters
            }
        };
    }
}
