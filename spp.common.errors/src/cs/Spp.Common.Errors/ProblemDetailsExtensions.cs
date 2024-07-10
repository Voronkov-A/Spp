using Microsoft.AspNetCore.Mvc;
using System;

namespace Spp.Common.Errors;

public static class ProblemDetailsExtensions
{
    public static int GetStatus(this ProblemDetails problemDetails)
    {
        return problemDetails.Status ?? throw new InvalidOperationException("Problem details status is null.");
    }
}
