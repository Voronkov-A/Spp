using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using IntegrationMocks.Core;
using IntegrationMocks.Core.Names;
using IntegrationMocks.Core.Networking;
using IntegrationMocks.Modules.Sql;
using IntegrationMocks.Modules.Testcontainers;
using Spp.IdentityProvider.TestClient.Service.V1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Miscellaneous.Docker;

namespace Spp.IdentityProvider.TestServer;

public sealed class DockerIdentityProviderService : IInfrastructureService<IdentityProviderContract>
{
    private readonly IPort _webApiPort;
    private readonly IContainer _container;

    public DockerIdentityProviderService(
        INameGenerator nameGenerator,
        IPortManager portManager,
        IInfrastructureService<SqlServiceContract> yugabyte,
        IEnumerable<DefaultUserSettings>? defaultUsers = null)
    {
        _webApiPort = portManager.TakePort();
        var webApiUrl = new Uri($"https://localhost:{_webApiPort.Number}");

        var defaultUserList = defaultUsers == null
            ? new List<DefaultUserSettings>
            {
                new()
                {
                    Username = "superuser",
                    Password = "pass"
                }
            }
            : defaultUsers.ToList();

        var defaultUserSet = new DefaultUserSetSettings
        {
            Users = defaultUserList
        };
        var configuration = new IdentityProviderConfiguration
        {
            DefaultUserSet = defaultUserSet
        };

        Contract = new IdentityProviderContract(webApiUrl, configuration);
        Debug.AutoFlush = true;
        var builder = new ContainerBuilder()
            .WithImage("localhost:51443/spp/identity-provider:0.0.1")
            .WithName(nameGenerator.GenerateName())
            .WithPortBinding(_webApiPort.Number, 32005)
            .WithEnvironment("Logging__Console__IncludeScopes", true.ToString())
            .WithEnvironment("Authentication__Issuers__0", webApiUrl.ToString().TrimEnd('/'))
            .WithEnvironment(
                "Persistence__Connection__Hostname",
                DockerNetwork.OverwriteHostname(yugabyte.Contract.Host))
            .WithEnvironment("Persistence__Connection__Port", yugabyte.Contract.Port.ToString())
            .WithEnvironment("Persistence__Connection__Username", yugabyte.Contract.Username)
            .WithEnvironment("Persistence__Connection__Password", yugabyte.Contract.Password)
            .WithEnvironment("Persistence__Connection__Database", nameGenerator.GenerateName());

        for (var i = 0; i < defaultUserList.Count; ++i)
        {
            var user = defaultUserList[i];
            builder = builder
                .WithEnvironment($"Application__DefaultUserSet__Users__{i}__Username", user.Username)
                .WithEnvironment($"Application__DefaultUserSet__Users__{i}__Password", user.Password);

            if (user.DefaultId != null)
            {
                builder = builder
                    .WithEnvironment($"Application__DefaultUserSet__Users__{i}__DefaultId", user.DefaultId);
            }
        }

        _container = builder
            .WithAutoRemove(true)
            .WithWaitStrategy(Wait.ForUnixContainer().AddCustomWaitStrategy(new WaitStrategy(webApiUrl)))
            .WithOutput<ContainerBuilder, IContainer>()
            .Build();
    }

    public IdentityProviderContract Contract { get; }

    public void Dispose()
    {
        DisposeAsync().AsTask().GetAwaiter().GetResult();
    }

    public async ValueTask DisposeAsync()
    {
        _webApiPort.Dispose();
        await _container.DisposeAsync();
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        await _container.StartAsync(cancellationToken);
    }

    private class WaitStrategy(Uri webApiUrl) : IWaitUntil
    {
        public async Task<bool> UntilAsync(IContainer container)
        {
            using var client = new ServiceTestClient(webApiUrl);

            try
            {
                var response = await client.HealthCheck();
                return response.StatusCode == HttpStatusCode.OK && response.Content200 == HealthStatus.Healthy;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
    }
}
