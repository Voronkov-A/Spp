using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Spp.Common.Authentication.TokenAuthenticationScheme.IdentityServer;

public static class AuthenticationBuilderExtensions
{
    public static AuthenticationBuilder AddIdentityServerToken(
        this AuthenticationBuilder builder,
        string authenticationScheme,
        string? displayName,
        Action<TokenAuthenticationOptions>? configureOptions,
        Action<TokenAccessorOptions>? configureTokenAccessor,
        Action<IdentityServerTokenGeneratorOptions>? configureTokenGenerator)
    {
        if (configureTokenAccessor != null)
        {
            builder.Services.Configure(configureTokenAccessor);
        }

        if (configureTokenGenerator != null)
        {
            builder.Services.Configure(configureTokenGenerator);
        }

        builder.Services
            .AddSingleton<ITokenAccessor, TokenAccessor>()
            .AddScoped<IdentityServerTokenParser>()
            .AddScoped<IdentityServerTokenGenerator>()
            .AddSingleton<ITokenParser, ScopedTokenParser<IdentityServerTokenParser>>()
            .AddSingleton<ITokenGenerator, ScopedTokenGenerator<IdentityServerTokenGenerator>>();
        return builder.AddScheme<TokenAuthenticationOptions, TokenAuthenticationHandler>(
            authenticationScheme,
            displayName,
            configureOptions);
    }

    public static AuthenticationBuilder AddIdentityServerToken(
        this AuthenticationBuilder builder,
        Action<TokenAuthenticationOptions>? configureOptions,
        Action<TokenAccessorOptions>? configureTokenAccessor,
        Action<IdentityServerTokenGeneratorOptions>? configureTokenGenerator)
    {
        return AddIdentityServerToken(
            builder,
            TokenAuthenticationDefaults.AuthenticationScheme,
            null,
            configureOptions,
            configureTokenAccessor,
            configureTokenGenerator);
    }
}
