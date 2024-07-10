using IntegrationMocks.Core;
using IntegrationMocks.Core.Names;
using IntegrationMocks.Core.Networking;
using IntegrationMocks.Modules.AspNetCore;
using IntegrationMocks.Modules.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spp.Authorization.Application.Auth.Settings;
using Microsoft.IdentityModel.Logging;
using Spp.Authorization.Application.Users.Settings;
using Spp.Authorization.WebApi.Callback.V1;
using Spp.IdentityProvider.TestServer;
using Spp.Common.Hosting;

namespace Spp.Authorization.Tests.TestHelpers.Services;

public class AuthorizationService : WebApplicationService<AuthorizationContract>
{
    private readonly IInfrastructureService<SqlServiceContract> _postgres;
    private readonly IInfrastructureService<IdentityProviderContract> _identityProvider;
    private readonly string _persistenceDatabaseName;
    private readonly IPort _webApiPort;

    public AuthorizationService(
        INameGenerator nameGenerator,
        IPortManager portManager,
        IInfrastructureService<SqlServiceContract> postgres,
        IInfrastructureService<IdentityProviderContract> identityProvider,
        IEnumerable<SuperuserSettings> superusers)
    {
        IdentityModelEventSource.ShowPII = true;

        _postgres = postgres;
        _identityProvider = identityProvider;
        _persistenceDatabaseName = nameGenerator.GenerateName();
        _webApiPort = portManager.TakePort();
        var webApiUrl = new Uri($"https://localhost:{_webApiPort.Number}");

        var configuration = new AuthorizationConfiguration
        {
            Authentication = new AuthenticationSettings
            {
                ClientId = "spp",
                ClientSecret = "secret",
                RedirectUris = new Uri[]
                {
                    new(webApiUrl, CallbackPaths.Callback)
                },
                Audience = webApiUrl.ToString().TrimEnd('/'),
                Issuers = new[]
                {
                    webApiUrl.ToString().TrimEnd('/')
                },
                KeyPath = "Certificates/localhost.crt"
            },
            SuperuserSet = new SuperuserSetSettings
            {
                Items = superusers.ToList()
            }
        };

        Contract = new AuthorizationContract(webApiUrl, configuration);
    }

    public override AuthorizationContract Contract { get; }

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
        var configuration = new Dictionary<string, string?>
        {
            ["Kestrel:EndPoints:Https:Url"] = Contract.WebApiUrl.ToString(),
            ["Authentication:ClientId"] = Contract.Configuration.Authentication.ClientId,
            ["Authentication:ClientSecret"] = Contract.Configuration.Authentication.ClientSecret,
            ["Authentication:RedirectUris:0"]
                = Contract.Configuration.Authentication.RedirectUris.Single().ToString().Trim('/'),
            ["Authentication:Audience"] = Contract.Configuration.Authentication.Audience,
            ["Authentication:Issuers:0"] = Contract.Configuration.Authentication.Issuers.Single(),
            ["Authentication:KeyPath"] = Contract.Configuration.Authentication.KeyPath,
            ["Authentication:Url"] = Contract.WebApiUrl.ToString(),
            ["Logging:LogLevel:Default"] = "Trace",
            ["Logging:Console:Enabled"] = true.ToString(),
            ["Logging:Console:IncludeScopes"] = true.ToString(),
            ["Logging:Debug:Enabled"] = true.ToString(),
            ["Logging:Debug:IncludeScopes"] = true.ToString(),
            ["Persistence:Connection:Hostname"] = _postgres.Contract.Host,
            ["Persistence:Connection:Port"] = _postgres.Contract.Port.ToString(),
            ["Persistence:Connection:Username"] = _postgres.Contract.Username,
            ["Persistence:Connection:Password"] = _postgres.Contract.Password,
            ["Persistence:Connection:Database"] = _persistenceDatabaseName,
            ["IdentityProvider:Authority"] = _identityProvider.Contract.WebApiUrl.ToString().TrimEnd('/'),
            ["IdentityProvider:RedirectUris:0"]
                = Contract.Configuration.Authentication.RedirectUris.Single().ToString().Trim('/'),
            ["IdentityProvider:Url"] = _identityProvider.Contract.WebApiUrl.ToString().TrimEnd('/')
        };

        foreach (var (user, i) in Contract.Configuration.SuperuserSet.Items.Select((x, i) => (x, i)))
        {
            foreach (var (identity, j) in user.Identities.Select((x, j) => (x, j)))
            {
                configuration[$"Application:SuperuserSet:Items:{i}:Identities:{j}:ProviderId"] = identity.ProviderId;
                configuration[$"Application:SuperuserSet:Items:{i}:Identities:{j}:SubjectId"] = identity.SubjectId;
            }
        }

        return DefaultHost.CreateBuilder()
            .WithConfiguration(builder => builder
                .AddJsonFile("appsettings.json")
                .AddInMemoryCollection(configuration))
            .WithDefaultLogging()
            .WithServices(AuthorizationStartup.ConfigureServices);

    }

    protected override void Configure(WebApplication app)
    {
        app.WithMiddlewares(AuthorizationStartup.Configure);
    }
}
