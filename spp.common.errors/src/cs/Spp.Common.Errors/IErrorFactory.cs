using Microsoft.AspNetCore.Mvc;
using System;

namespace Spp.Common.Errors;

public interface IErrorFactory
{
    ProblemDetails CreateError<TErrorCode>(
        TErrorCode code,
        int status,
        string title,
        string detail,
        object? parameters)
        where TErrorCode : struct, Enum;
}
