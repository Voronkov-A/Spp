using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spp.Authorization.Application;
using Spp.Authorization.Initialization;
using Spp.Authorization.Persistence;
using Spp.Authorization.WebApi;
using Spp.Authorization.WebApi.Service.V1;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Duende.IdentityServer;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Spp.Authorization.Application.Auth;
using Spp.Authorization.Application.Auth.Settings;
using Spp.Authorization.Domain.Users;
using Spp.Authorization.WebApi.Callback.V1;
using Spp.IdentityProvider.Client.Applications.V1;
using System;
using Spp.Authorization.WebApi.Auth;
using Spp.Authorization.Domain;
using Spp.Common.Exceptions;
using Spp.Common.Authorization;
using Spp.Authorization.Client.Sdk.Domain;
using Spp.Common.Authentication.TokenAuthenticationScheme;
using Spp.Common.Authentication.TokenAuthenticationScheme.IdentityServer;
using Spp.Common.Authentication.Oidc;
using Spp.Common.Authentication.Abstractions;
using Spp.Common.Authentication.Http;
using Spp.Common.Configuration;
using Spp.Common.Authentication;
using Spp.Common.Mediator;
using Spp.Common.Hosting.HealthChecks;
using Spp.Common.Cqs;
using Spp.Common.Http;

namespace Spp.Authorization;

public static class AuthorizationStartup
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSettings<AuthenticationSettings>("Authentication");
        var authenticationSettings = configuration.GetSettings<AuthenticationSettings>("Authentication");

        services.AddSettings<IdentityProviderSettings>("IdentityProvider");
        var identityProviderSettings = configuration.GetSettings<IdentityProviderSettings>("IdentityProvider");

        var cert = new X509Certificate2(authenticationSettings.KeyPath);

        services.Configure<IdentityOptions>(options =>
        {
            options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
            options.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.Name;
            options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
        });

        services.AddTransient<IUserClaimsPrincipalFactory<User>, IdentityProviderUserClaimsPrincipalFactory>();

        var builder = services
            .AddIdentityServer(options =>
            {
                options.UserInteraction = new UserInteractionOptions
                {
                    LogoutUrl = "/account/logout",
                    LoginUrl = "/account/login",
                    LoginReturnUrlParameter = "returnUrl"
                };
            })
            .AddSigningCredentials()
            .AddInMemoryIdentityResources(new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            })
            .AddInMemoryApiResources(new[]
            {
                new ApiResource(authenticationSettings.Audience)
                {
                    Scopes = new[]
                    {
                        Scopes.Api
                    },
                    UserClaims = new[]
                    {
                        SuperuserClaim.Type,
                        RoleClaim.Type
                    }
                }
            })
            .AddInMemoryApiScopes(new[]
            {
                new ApiScope(Scopes.Api)
                {
                    UserClaims = new[]
                    {
                        SuperuserClaim.Type,
                        RoleClaim.Type
                    }
                }
            })
            .AddInMemoryClients(new[]
            {
                new Duende.IdentityServer.Models.Client
                {
                    ClientId = authenticationSettings.ClientId,
                    ClientSecrets = new[]
                    {
                        new Secret(authenticationSettings.ClientSecret.Sha256())
                    },
                    AllowedGrantTypes = new[]
                    {
                        GrantType.ClientCredentials,
                        GrantType.AuthorizationCode
                    },
                    AllowedScopes = Scopes.Enumerate().ToList(),
                    AllowOfflineAccess = true,
                    RedirectUris = authenticationSettings.RedirectUris
                        .SelectMany(x => new[] { x.ToString(), x.ToString().TrimEnd('/') })
                        .ToList(),
                    ProtocolType = IdentityServerConstants.ProtocolTypes.OpenIdConnect
                }
            });
        services.Configure<IdentityServerOptions>(o => o.Authentication.CookieAuthenticationScheme = null);

        services
            .AddAuthentication(TokenAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(options =>
            {
                options.MapInboundClaims = false;
                options.SignInScheme = TokenAuthenticationDefaults.AuthenticationScheme;
                options.Authority = identityProviderSettings.Authority;
                options.ClientId = identityProviderSettings.ClientId;
                options.ClientSecret = identityProviderSettings.ClientSecret;
                options.RequireHttpsMetadata = true;
                options.ResponseType = OidcConstants.ResponseTypes.Code;
                options.UsePkce = true;

                foreach (var scope in identityProviderSettings.Scope.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    options.Scope.Add(scope);
                }

                options.CallbackPath = CallbackPaths.Callback;
                options.ResponseMode = OidcConstants.ResponseModes.Query;
            })
            .AddIdentityServerToken(
                options =>
                {
                    options.Events = new TokenAuthenticationEvents
                    {
                        OnChallenge = TokenAuthenticationEventHandlers.OnChallenge,
                        OnForbidden = TokenAuthenticationEventHandlers.OnForbidden,
                        OnSigningIn = TokenAuthenticationEventHandlers.OnSigningIn
                    };
                },
                options =>
                {
                    options.AccessTokenCookieName = AccessTokenCookie.Name;
                    options.RefreshTokenCookieName = RefreshTokenCookie.Name;
                },
                options =>
                {
                    options.ClientId = authenticationSettings.ClientId;
                    options.ClientSecret = authenticationSettings.ClientSecret;
                    options.DefaultScope = Scopes.All;
                    options.ScopePropertyKey = AuthenticationPropertyKeys.Scope;
                });

        services.AddExceptions();
        services.AddHttp();
        services.AddDomain();
        services.AddApplication();
        services.AddPersistence();
        services.AddWebApi();
        services.AddInitialization();

        services
            .AddSettings<ClientCredentialsAccessTokenAcquirerSettings<ApplicationRegistrationAuthenticationContext>>(
                "IdentityProvider");
        services
            .AddHttpClient<
                IAccessTokenAcquirer<ApplicationRegistrationAuthenticationContext>,
                ClientCredentialsAccessTokenAcquirer<ApplicationRegistrationAuthenticationContext>>(
                c => c.BaseAddress = identityProviderSettings.Url);
        services
            .AddSingleton<AccessTokenCache>()
            .AddTransient<BearerAuthenticationHttpMessageHandler<ApplicationRegistrationAuthenticationContext>>()
            .AddHttpClient<IApplicationsClient, ApplicationsClient>(c => c.BaseAddress = identityProviderSettings.Url)
            .AddHttpMessageHandler<
                BearerAuthenticationHttpMessageHandler<ApplicationRegistrationAuthenticationContext>>();
        services
            .AddSingleton<
                IAuthenticationContext<ApplicationRegistrationAuthenticationContext>,
                ApplicationRegistrationAuthenticationContext>();

        services.AddScoped(typeof(IRequestHandlerDecorator<,>), typeof(UnitOfWorkRequestHandlerDecorator<,>));
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
