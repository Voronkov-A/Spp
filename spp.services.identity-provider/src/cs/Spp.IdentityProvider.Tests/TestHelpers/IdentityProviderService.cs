using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegrationMocks.Core;
using IntegrationMocks.Core.Names;
using IntegrationMocks.Core.Networking;
using IntegrationMocks.Modules.AspNetCore;
using IntegrationMocks.Modules.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Spp.Common.Hosting;
using Spp.IdentityProvider.Application.Applications.Settings;
using Spp.IdentityProvider.Application.Users.Settings;

namespace Spp.IdentityProvider.Tests.TestHelpers;

public class IdentityProviderService : WebApplicationService<IdentityProviderContract>
{
    private readonly IInfrastructureService<SqlServiceContract> _yugabyte;
    private readonly string _persistenceDatabaseName;
    private readonly IPort _webApiPort;

    public IdentityProviderService(
        INameGenerator nameGenerator,
        IPortManager portManager,
        IInfrastructureService<SqlServiceContract> yugabyte)
    {
        IdentityModelEventSource.ShowPII = true;

        _yugabyte = yugabyte;
        _persistenceDatabaseName = nameGenerator.GenerateName();
        _webApiPort = portManager.TakePort();
        var webApiUrl = new Uri($"https://localhost:{_webApiPort.Number}");
        var defaultApplication = new DefaultApplicationSettings
        {
            ClientId = "identity_provider",
            ClientSecret = "secret",
            RedirectUris = new Uri[]
            {
                new(webApiUrl, "/v1/auth/callback")
            }
        };
        var defaultUserSet = new DefaultUserSetSettings
        {
            Users = new DefaultUserSettings[]
            {
                new()
                {
                    Username = "superuser",
                    Password = "pass"
                }
            }
        };
        var configuration = new IdentityProviderConfiguration
        {
            DefaultApplication = defaultApplication,
            DefaultUserSet = defaultUserSet
        };
        Contract = new IdentityProviderContract(webApiUrl, configuration);
    }

    public override IdentityProviderContract Contract { get; }

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
        return DefaultHost.CreateBuilder()
            .WithConfiguration(builder => builder
                .AddJsonFile("appsettings.json")
                .AddInMemoryCollection(
                    new Dictionary<string, string?>
                    {
                        ["Kestrel:EndPoints:Https:Url"] = Contract.WebApiUrl.ToString(),
                        ["Authentication:Issuers:0"] = Contract.WebApiUrl.ToString().TrimEnd('/'),
                        ["Logging:Console:Enabled"] = true.ToString(),
                        ["Logging:Console:IncludeScopes"] = true.ToString(),
                        ["Logging:Debug:Enabled"] = true.ToString(),
                        ["Logging:Debug:IncludeScopes"] = true.ToString(),
                        ["Persistence:Connection:Hostname"] = _yugabyte.Contract.Host,
                        ["Persistence:Connection:Port"] = _yugabyte.Contract.Port.ToString(),
                        ["Persistence:Connection:Username"] = _yugabyte.Contract.Username,
                        ["Persistence:Connection:Password"] = _yugabyte.Contract.Password,
                        ["Persistence:Connection:Database"] = _persistenceDatabaseName,
                        ["Application:DefaultApplication:ClientId"]
                            = Contract.Configuration.DefaultApplication.ClientId,
                        ["Application:DefaultApplication:ClientSecret"]
                            = Contract.Configuration.DefaultApplication.ClientSecret,
                        ["Application:DefaultApplication:RedirectUris:0"]
                            = Contract.Configuration.DefaultApplication.RedirectUris.First().ToString(),
                        ["Application:DefaultUserSet:Users:0:Username"]
                            = Contract.Configuration.DefaultUserSet.Users.First().Username,
                        ["Application:DefaultUserSet:Users:0:Password"]
                            = Contract.Configuration.DefaultUserSet.Users.First().Password
                    }))
            .WithDefaultLogging()
            .WithServices(IdentityProviderStartup.ConfigureServices);

    }

    protected override void Configure(WebApplication app)
    {
        app.WithMiddlewares(IdentityProviderStartup.Configure);
    }
}
