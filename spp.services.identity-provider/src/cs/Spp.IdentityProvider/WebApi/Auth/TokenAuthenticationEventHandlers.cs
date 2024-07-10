using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Spp.Common.Authentication.TokenAuthenticationScheme;
using Spp.Common.Errors;
using System;
using System.Threading.Tasks;

namespace Spp.IdentityProvider.WebApi.Auth;

public static class TokenAuthenticationEventHandlers
{
    public static Task OnChallenge(TokenChallengeContext context)
    {
        FailAuthentication(context, "User credentials are invalid or not provided.");
        context.Handled = true;
        return Task.CompletedTask;
    }

    public static Task OnForbidden(TokenForbiddenContext context)
    {
        FailAuthorization(context, "Access to the resource is denied.");
        context.Handled = true;
        return Task.CompletedTask;
    }

    private static void FailAuthentication(PropertiesContext<TokenAuthenticationOptions> context, string detail)
    {
        Fail(context, detail, static (f, d) => f.AuthenticationFailure(d));
    }

    private static void FailAuthorization(PropertiesContext<TokenAuthenticationOptions> context, string detail)
    {
        Fail(context, detail, static (f, d) => f.AuthorizationFailure(d));
    }

    private static void Fail(
        PropertiesContext<TokenAuthenticationOptions> context,
        string detail,
        Func<ICommonErrorFactory, string, ProblemDetails> createError)
    {
        var serviceProvider = context.HttpContext.RequestServices;
        var writer = serviceProvider.GetRequiredService<IProblemDetailsWriter>();
        var errorFactory = serviceProvider.GetRequiredService<ICommonErrorFactory>();

        var error = createError(errorFactory, detail);

        context.HttpContext.Response.OnStarting(async () =>
        {
            context.HttpContext.Response.StatusCode = error.GetStatus();
            await writer.WriteAsync(new ProblemDetailsContext
            {
                HttpContext = context.HttpContext,
                ProblemDetails = error
            });
        });
    }
}
