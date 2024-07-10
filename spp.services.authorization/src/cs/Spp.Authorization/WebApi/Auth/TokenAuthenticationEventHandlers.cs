using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Spp.Authorization.Application.Users.Commands;
using Spp.Authorization.Application.Users.Models;
using Spp.Authorization.Domain.Users;
using Spp.Common.Authentication.TokenAuthenticationScheme;
using Spp.Common.Errors;
using Spp.Common.Mediator;
using System;
using System.Threading.Tasks;

namespace Spp.Authorization.WebApi.Auth;

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

    public static async Task OnSigningIn(TokenSigningInContext context)
    {
        var serviceProvider = context.HttpContext.RequestServices;
        var handler = serviceProvider
            .GetRequiredService<IRequestHandler<CreateUserIfNotExistsCommand, CreateUserIfNotExistsResponse>>();

        var providerId = context.Properties.GetProviderId();
        var subjectId = context.User.GetSubjectId();
        var identity = new UserIdentity(providerId: providerId, subjectId: subjectId);
        var user = await handler.Handle(new CreateUserIfNotExistsCommand(identity), context.HttpContext.RequestAborted);

        if (user.IsBlocked)
        {
            FailAuthentication(context, "User is blocked.");
            context.Handled = true;
            return;
        }

        context.User = user.ClaimsPrincipal;
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
