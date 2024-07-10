using IntegrationMocks.Core;
using Spp.Authorization.TestClient.Service.V1;
using Spp.Authorization.Tests.TestHelpers.Collections;
using Spp.Authorization.Tests.TestHelpers.Fixtures;
using Spp.Authorization.Tests.TestHelpers.Services;
using System.Threading.Tasks;
using Xunit;

namespace Spp.Authorization.Tests.Service;

[Collection(nameof(AuthorizationServiceCollection))]
public class AuthorizationServiceTests(AuthorizationServiceFixture authorizationServiceFixture)
{
    private readonly IInfrastructureService<AuthorizationContract> _authorization
        = authorizationServiceFixture.Authorization;

    [Fact]
    public async Task Health_check_works()
    {
        // ARRANGE
        using var client = new ServiceTestClient(_authorization.Contract.WebApiUrl);

        // ACT
        var healthStatus = (await client.HealthCheck()).Content200;

        // ASSERT
        Assert.Equal(HealthStatus.Healthy, healthStatus);
    }
}
