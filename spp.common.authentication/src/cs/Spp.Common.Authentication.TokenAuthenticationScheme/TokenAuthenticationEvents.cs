using System;
using System.Threading.Tasks;

namespace Spp.Common.Authentication.TokenAuthenticationScheme;

public class TokenAuthenticationEvents
{
    public Func<TokenChallengeContext, Task> OnChallenge { get; set; } = context => Task.CompletedTask;

    public Func<TokenSigningInContext, Task> OnSigningIn { get; set; } = context => Task.CompletedTask;

    public Func<TokenForbiddenContext, Task> OnForbidden { get; set; } = context => Task.CompletedTask;

    public virtual Task Challenge(TokenChallengeContext context) => OnChallenge(context);

    public virtual Task SigningIn(TokenSigningInContext context) => OnSigningIn(context);

    public virtual Task Forbidden(TokenForbiddenContext context) => OnForbidden(context);
}
