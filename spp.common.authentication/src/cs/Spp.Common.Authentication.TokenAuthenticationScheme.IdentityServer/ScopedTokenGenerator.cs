using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Spp.Common.Authentication.TokenAuthenticationScheme.IdentityServer;

public class ScopedTokenGenerator<TInner>(IServiceProvider services) : ITokenGenerator where TInner : ITokenGenerator
{
    private readonly IServiceProvider _services = services;

    public async Task<TokenGenerationResult> GenerateTokens(
        ClaimsPrincipal user,
        AuthenticationProperties? properties,
        CancellationToken cancellationToken)
    {
        using var scope = _services.CreateScope();
        var inner = scope.ServiceProvider.GetRequiredService<TInner>();
        return await inner.GenerateTokens(user, properties, cancellationToken);
    }
}
