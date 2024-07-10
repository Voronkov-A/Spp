using IdentityModel;
using IntegrationMocks.Core;
using IntegrationMocks.Core.Networking;
using IntegrationMocks.Modules.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Spp.Common.Errors;
using Spp.Common.Hosting;
using Spp.Common.Initialization;
using Spp.IdentityProvider.Client.Applications.V1;
using Spp.IdentityProvider.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.IdentityProvider.Tests.TestHelpers;

public class SampleClientApplicationService : MockWebApplicationService<SampleClientApplicationContract>
{
    public const string ResourcePath = "/resource";
    public const string SignInPath = "/sign-in";
    private const string CallbackPath = "/callback";
    private const string ClientId = nameof(SampleClientApplicationService);
    private const string ClientSecret = "secret";

    private readonly IPort _webApiPort;
    private readonly IInfrastructureService<IdentityProviderContract> _identityProvider;

    public SampleClientApplicationService(
        IPortManager portManager,
        IInfrastructureService<IdentityProviderContract> identityProvider)
        : base(portManager)
    {
        IdentityModelEventSource.ShowPII = true;

        _webApiPort = portManager.TakePort();
        _identityProvider = identityProvider;
        Contract = new SampleClientApplicationContract(new Uri($"https://localhost:{_webApiPort.Number}"));

        AddController(typeof(SampleController));
    }

    public override SampleClientApplicationContract Contract { get; }

    protected override async ValueTask DisposeAsync(bool disposing)
    {
        await base.DisposeAsync(disposing);

        if (disposing)
        {
            _webApiPort.Dispose();
        }
    }

    protected override WebApplicationBuilder CreateWebApplicationBuilder()
    {
        return base.CreateWebApplicationBuilder()
            .WithConfiguration(builder => builder
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Kestrel:EndPoints:Https:Url"] = Contract.WebApiUrl.ToString(),
                    ["Logging:Properties:ApplicationName"] = "sample_client_application",
                    ["Logging:Console:Enabled"] = true.ToString(),
                    ["Logging:Console:IncludeScopes"] = true.ToString(),
                    ["Logging:Debug:Enabled"] = true.ToString(),
                    ["Logging:Debug:IncludeScopes"] = true.ToString()
                }))
            .WithDefaultLogging()
            .WithServices(services =>
            {
                services
                    .AddHttpClient<IApplicationsClient, ApplicationsClient>(
                        c => c.BaseAddress = _identityProvider.Contract.WebApiUrl);

                services
                    .AddErrors()
                    .AddAuthentication(options =>
                    {
                        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    })
                    .AddCookie(options =>
                    {
                        options.Events.OnRedirectToLogin = context =>
                        {
                            var errorFactory
                                = context.HttpContext.RequestServices.GetRequiredService<ICommonErrorFactory>();
                            var writer
                                = context.HttpContext.RequestServices.GetRequiredService<IProblemDetailsWriter>();
                            var error = errorFactory.AuthenticationFailure("");
                            context.Response.OnStarting(async () =>
                            {
                                context.Response.StatusCode = error.GetStatus();
                                await writer.WriteAsync(new ProblemDetailsContext
                                {
                                    HttpContext = context.HttpContext,
                                    ProblemDetails = error
                                });
                            });
                            return Task.CompletedTask;
                        };
                    })
                    .AddOpenIdConnect(options =>
                    {
                        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        options.Authority = _identityProvider.Contract.WebApiUrl.ToString().TrimEnd('/');
                        options.ClientId = ClientId;
                        options.ClientSecret = ClientSecret;
                        options.RequireHttpsMetadata = true;
                        options.ResponseType = OidcConstants.ResponseTypes.Code;
                        options.UsePkce = true;

                        foreach (var scope in Scopes.Enumerate())
                        {
                            options.Scope.Add(scope);
                        }

                        options.CallbackPath = CallbackPath;
                        options.ResponseMode = OidcConstants.ResponseModes.Query;
                    });

                services.Configure<ApplicationRegistrationSettings>(x =>
                {
                    x.RedirectUris = new Uri[]
                    {
                        new(Contract.WebApiUrl, CallbackPath)
                    };
                });
                services.AddInitializer<ApplicationRegistrationInitializer>();
            });
    }

    protected override void Configure(WebApplication app)
    {
        base.Configure(app);

        app.UseAuthentication();
        app.UseAuthorization();
    }

    [ApiController]
    private class SampleController : Controller
    {
        [HttpGet(ResourcePath), Authorize]
        public Task Resource()
        {
            Response.StatusCode = (int) HttpStatusCode.NoContent;
            return Task.CompletedTask;
        }

        [HttpGet(SignInPath)]
        public Task<IActionResult> SignIn([FromQuery(Name = "redirect_uri")] string redirectUri)
        {
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = redirectUri
            };
            IActionResult challengeResult = Challenge(
                authenticationProperties,
                OpenIdConnectDefaults.AuthenticationScheme);
            return Task.FromResult(challengeResult);
        }
    }

    private class ApplicationRegistrationSettings
    {
        public required IReadOnlyCollection<Uri> RedirectUris { get; set; } = [];
    }

    private class ApplicationRegistrationInitializer(
        IApplicationsClient applicationsClient,
        IOptions<ApplicationRegistrationSettings> settings)
        : IInitializer
    {
        public async Task Initialize(CancellationToken cancellationToken)
        {
            await applicationsClient.Create(
                new CreateApplicationRequest(
                    clientId: ClientId,
                    clientSecret: ClientSecret,
                    redirectUris: settings.Value.RedirectUris.ToList()),
                cancellationToken);
        }
    }
}
