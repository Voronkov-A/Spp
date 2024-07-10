using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Spp.Common.Authentication.TokenAuthenticationScheme.IdentityServer;

public class ScopedTokenParser<TInner>(IServiceProvider services) : ITokenParser where TInner : ITokenParser
{
    private readonly IServiceProvider _services = services;

    public async Task<TokenParseResult> Parse(string token, CancellationToken cancellationToken)
    {
        using var scope = _services.CreateScope();
        var inner = scope.ServiceProvider.GetRequiredService<TInner>();
        return await inner.Parse(token, cancellationToken);
    }
}