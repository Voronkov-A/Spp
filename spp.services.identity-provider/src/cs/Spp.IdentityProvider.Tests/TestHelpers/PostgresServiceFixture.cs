using IntegrationMocks.Core;
using IntegrationMocks.Modules.Sql;
using IntegrationMocks.Modules.Yugabyte;
using System.Threading.Tasks;
using Xunit;

namespace Spp.IdentityProvider.Tests.TestHelpers;

public class PostgresServiceFixture : IAsyncLifetime
{
    public PostgresServiceFixture()
    {
        Yugabyte = new DockerYugabyteService();
    }

    public IInfrastructureService<SqlServiceContract> Yugabyte { get; }

    public async Task DisposeAsync()
    {
        await Yugabyte.DisposeAsync();
    }

    public async Task InitializeAsync()
    {
        await Yugabyte.InitializeAsync();
    }
}
