using Microsoft.Extensions.DependencyInjection;

namespace Spp.Common.Authentication;

public static class ServiceCollectionExtensions
{
    /*public static IServiceCollection AddDefaultAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication(TokenAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerToken(
                options =>
                {
                    options.Events = new TokenAuthenticationEvents
                    {
                        OnChallenge = DefaultAuthenticationEventHandlers.OnChallenge,
                        OnForbidden = DefaultAuthenticationEventHandlers.OnForbidden
                    };
                },
                options =>
                {
                    options.AccessTokenCookieName = AccessTokenCookie.Name;
                    options.RefreshTokenCookieName = RefreshTokenCookie.Name;
                })

    }*/
}
