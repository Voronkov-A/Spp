using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Spp.Common.Authentication.TokenAuthenticationScheme;

public class TokenAuthenticationHandler(
    IOptionsMonitor<TokenAuthenticationOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ITokenAccessor tokenAccessor,
    ITokenParser tokenParser,
    ITokenGenerator tokenGenerator) :
    SignInAuthenticationHandler<TokenAuthenticationOptions>(options, logger, encoder)
{
    protected new TokenAuthenticationEvents Events
    {
        get => (TokenAuthenticationEvents)base.Events!;
        set => base.Events = value;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var accessToken = tokenAccessor.GetAccessToken(Request);

        if (accessToken == null)
        {
            return AuthenticateResult.Fail("No access token provided.");
        }

        var tokenParseResult = await tokenParser.Parse(accessToken, Context.RequestAborted);

        if (!tokenParseResult.IsSucceeded)
        {
            return AuthenticateResult.Fail(tokenParseResult.Error);
        }

        var principal = new ClaimsPrincipal(new[]
        {
            new ClaimsIdentity(tokenParseResult.Value.Claims, Scheme.Name)
        });
        return AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name));
    }

    protected override async Task HandleSignInAsync(ClaimsPrincipal user, AuthenticationProperties? properties)
    {
        var signingInContext = new TokenSigningInContext(Context, Scheme, Options, properties, user);
        await Events.SigningIn(signingInContext);

        if (signingInContext.Handled)
        {
            return;
        }

        var tokens = await tokenGenerator.GenerateTokens(signingInContext.User, properties, Context.RequestAborted);
        tokenAccessor.SetAccessToken(Response, tokens.AccessToken);

        if (tokens.RefreshToken != null)
        {
            tokenAccessor.SetRefreshToken(Response, tokens.RefreshToken);
        }
    }

    protected override Task HandleSignOutAsync(AuthenticationProperties? properties)
    {
        tokenAccessor.DeleteTokens(Response);
        return Task.CompletedTask;
    }

    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        var challengeContext = new TokenChallengeContext(Context, Scheme, Options, properties);
        await Events.Challenge(challengeContext);

        if (challengeContext.Handled)
        {
            return;
        }

        Response.StatusCode = (int)HttpStatusCode.Unauthorized;
    }

    protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        var forbiddenContext = new TokenForbiddenContext(Context, Scheme, Options, properties);
        await Events.Forbidden(forbiddenContext);

        if (forbiddenContext.Handled)
        {
            return;
        }

        await base.HandleForbiddenAsync(properties);
    }
}
