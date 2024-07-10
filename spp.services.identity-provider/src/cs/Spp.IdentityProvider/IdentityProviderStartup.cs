using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spp.IdentityProvider.Application;
using Spp.IdentityProvider.Application.Users;
using Spp.IdentityProvider.Domain.Users;
using Spp.IdentityProvider.Initialization;
using Spp.IdentityProvider.Persistence;
using Spp.IdentityProvider.Persistence.Authorization;
using Spp.IdentityProvider.WebApi.Service.V1;
using System.Security.Cryptography.X509Certificates;
using Spp.Common.Configuration;
using Spp.Common.Errors;
using Spp.Common.Hosting.HealthChecks;
using Spp.IdentityProvider.WebApi;
using Spp.IdentityProvider.WebApi.Users;
using Spp.IdentityProvider.WebApi.Auth;
using Spp.Common.Authentication.TokenAuthenticationScheme.IdentityServer;
using Spp.Common.Authentication.TokenAuthenticationScheme;
using Spp.IdentityProvider.Application.Applications.Settings;

namespace Spp.IdentityProvider;

public static class IdentityProviderStartup
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSettings<AuthenticationSettings>("Authentication");
        var authenticationSettings = configuration.GetSettings<AuthenticationSettings>("Authentication");
        var cert = new X509Certificate2(authenticationSettings.KeyPath);

        var defaultApplicationSettings = configuration.GetSettings<DefaultApplicationSettings>(
            "Application:DefaultApplication");

        services.AddOptions();
        services.AddHealthChecks();
        services.AddApplication();
        services.AddErrors();
        services.AddPersistence();

        services
            .AddDefaultIdentity<User>(options =>
            {
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<AuthorizationDbContext>();

        services
            .AddIdentityServer(options =>
            {
                options.UserInteraction = new UserInteractionOptions
                {
                    LogoutUrl = "/account/logout",
                    LoginUrl = "/account/login",
                    LoginReturnUrlParameter = "returnUrl"
                };
            })
            .AddApiAuthorization<User, AuthorizationDbContext>()
            .AddInMemoryApiResources(new[]
            {
                new ApiResource(authenticationSettings.Audience)
                {
                    Scopes = new[]
                    {
                        Scopes.Api
                    }
                }
            })
            .AddInMemoryApiScopes(new[]
            {
                new ApiScope(Scopes.Api)
            })
            .AddInMemoryIdentityResources(new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            })
            .AddClientStore<ClientStore>()
            .AddUserSession<ClaimsBasedUserSession>();

        services
            .AddControllers()
            .AddApplicationPart(typeof(IdentityProviderStartup).Assembly);

        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = TokenAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = TokenAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = TokenAuthenticationDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = TokenAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = TokenAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = TokenAuthenticationDefaults.AuthenticationScheme;
            })
            .AddIdentityServerToken(
                options =>
                {
                    options.Events = new TokenAuthenticationEvents
                    {
                        OnChallenge = TokenAuthenticationEventHandlers.OnChallenge,
                        OnForbidden = TokenAuthenticationEventHandlers.OnForbidden
                    };
                },
                options =>
                {
                    options.AccessTokenCookieName = AccessTokenCookie.Name;
                    options.RefreshTokenCookieName = RefreshTokenCookie.Name;
                },
                options =>
                {
                    options.ClientId = defaultApplicationSettings.ClientId;
                    options.ClientSecret = defaultApplicationSettings.ClientSecret;
                    options.DefaultScope = Scopes.All;
                    options.ScopePropertyKey = AuthenticationPropertyKeys.Scope;
                });

        services.AddTransient<IUserClaimsPrincipalFactory<User>, IdentityProviderUserClaimsPrincipalFactory>();
        services.AddWebApi();
        services.AddSingleton(cert);

        services.AddInitialization();
    }

    public static void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseDefaultHealthChecks(ServicePaths.HealthCheck);
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseIdentityServer();
        app.UseEndpoints(x => x.MapControllers());
    }
}
