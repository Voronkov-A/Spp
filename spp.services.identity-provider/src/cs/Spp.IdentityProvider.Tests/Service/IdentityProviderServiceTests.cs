using System.Threading.Tasks;
using IntegrationMocks.Core;
using Spp.IdentityProvider.TestClient.Service.V1;
using Spp.IdentityProvider.Tests.TestHelpers;
using Spp.IdentityProvider.Tests.TestHelpers.Collections;
using Xunit;

namespace Spp.IdentityProvider.Tests.Service;

[Collection(nameof(IdentityProviderServiceCollection))]
public class IdentityProviderServiceTests(IdentityProviderServiceFixture identityProviderFixture)
{
    private readonly IInfrastructureService<IdentityProviderContract> _identityProvider
        = identityProviderFixture.IdentityProvider;

    [Fact]
    public async Task Health_check_works()
    {
        // ARRANGE
        using var client = new ServiceTestClient(_identityProvider.Contract.WebApiUrl);

        // ACT
        var healthStatus = (await client.HealthCheck()).Content200;

        // ASSERT
        Assert.Equal(HealthStatus.Healthy, healthStatus);
    }
}
