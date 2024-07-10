using IntegrationMocks.Core;
using IntegrationMocks.Core.Names;
using IntegrationMocks.Core.Networking;
using IntegrationMocks.Modules.Yugabyte;
using Spp.IdentityProvider.TestClient.Service.V1;
using Spp.IdentityProvider.TestServer;
using System.Threading.Tasks;
using Xunit;

namespace Spp.IdentityProvider.Tests.TestServer;

public class DockerIdentityProviderServiceTests
{
    [Fact(Skip = "For manual use only.")]
    public async Task Health_check_works()
    {
        // ARRANGE
        var nameGenerator = new RandomNameGenerator(nameof(DockerIdentityProviderServiceTests));
        var portManager = PortManager.Default;
        await using var yugabyte = new DockerYugabyteService(nameGenerator, portManager);
        await using var identityProvider = new DockerIdentityProviderService(nameGenerator, portManager, yugabyte);
        await yugabyte.InitializeAsync();
        await identityProvider.InitializeAsync();
        using var client = new ServiceTestClient(identityProvider.Contract.WebApiUrl);

        // ACT
        var healthStatus = (await client.HealthCheck()).Content200;

        // ASSERT
        Assert.Equal(HealthStatus.Healthy, healthStatus);
    }
}
