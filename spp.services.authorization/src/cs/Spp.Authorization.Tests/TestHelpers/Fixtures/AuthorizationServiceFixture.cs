using IntegrationMocks.Core;
using IntegrationMocks.Core.Names;
using IntegrationMocks.Core.Networking;
using IntegrationMocks.Modules.Sql;
using IntegrationMocks.Modules.Yugabyte;
using Spp.Authorization.Tests.TestHelpers.Services;
using Spp.Common.Miscellaneous;
using Spp.IdentityProvider.TestServer;
using System.Threading.Tasks;
using Spp.Authorization.Application.Users.Settings;
using Spp.Authorization.WebApi.Auth;
using Xunit;
using Spp.Common.Domain;

namespace Spp.Authorization.Tests.TestHelpers.Fixtures;

public class AuthorizationServiceFixture : IAsyncLifetime
{
    public AuthorizationServiceFixture()
    {
        var nameGenerator = new RandomNameGenerator(nameof(AuthorizationServiceFixture));
        var portManager = PortManager.Default;
        Yugabyte = new DockerYugabyteService(nameGenerator, portManager);
        var superuserSubjectId = new EntityId().ToString();
        IdentityProvider = new DockerIdentityProviderService(
            nameGenerator,
            portManager,
            Yugabyte,
            new[]
            {
                new DefaultUserSettings
                {
                    Username = "superuser",
                    Password = "pass",
                    DefaultId = superuserSubjectId
                },
                new DefaultUserSettings
                {
                    Username = "user",
                    Password = "pass"
                }
            });
        Authorization = new AuthorizationService(
            nameGenerator,
            portManager,
            Yugabyte,
            IdentityProvider,
            new[]
            {
                new SuperuserSettings
                {
                    Identities = new[]
                    {
                        new SuperuserIdentitySettings
                        {
                            ProviderId = UserIdentityProviders.IdentityProviderId,
                            SubjectId = superuserSubjectId
                        }
                    }
                }
            });
    }

    public IInfrastructureService<SqlServiceContract> Yugabyte { get; }

    public IInfrastructureService<IdentityProviderContract> IdentityProvider { get; }

    public IInfrastructureService<AuthorizationContract> Authorization { get; }

    public async Task DisposeAsync()
    {
        await DisposableUtils.Dispose(Authorization);
        await DisposableUtils.Dispose(IdentityProvider);
        await DisposableUtils.Dispose(Yugabyte);
    }

    public async Task InitializeAsync()
    {
        await Yugabyte.InitializeAsync();
        await IdentityProvider.InitializeAsync();
        await Authorization.InitializeAsync();
    }
}
