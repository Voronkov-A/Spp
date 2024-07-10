using IntegrationMocks.Core;
using IntegrationMocks.Core.Networking;
using System.Threading.Tasks;
using IntegrationMocks.Core.Names;
using IntegrationMocks.Modules.Sql;
using IntegrationMocks.Modules.Yugabyte;
using Spp.Common.Miscellaneous;
using Xunit;

namespace Spp.IdentityProvider.Tests.TestHelpers;

public class IdentityProviderServiceFixture : IAsyncLifetime
{
    public IdentityProviderServiceFixture()
    {
        var portManager = PortManager.Default;
        var nameGenerator = new RandomNameGenerator(nameof(IdentityProviderServiceFixture));
        Yugabyte = new DockerYugabyteService(nameGenerator, portManager);
        IdentityProvider = new IdentityProviderService(nameGenerator, portManager, Yugabyte);
    }

    public IInfrastructureService<SqlServiceContract> Yugabyte { get; }

    public IInfrastructureService<IdentityProviderContract> IdentityProvider { get; }

    public async Task DisposeAsync()
    {
        await DisposableUtils.Dispose(IdentityProvider);
        await DisposableUtils.Dispose(Yugabyte);
    }

    public async Task InitializeAsync()
    {
        await Yugabyte.InitializeAsync();
        await IdentityProvider.InitializeAsync();
    }
}
