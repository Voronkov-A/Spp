using Spp.IdentityProvider.Application.Users.Commands;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Initialization;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;

namespace Spp.IdentityProvider.Initialization;

public class DefaultUserInitializer(IMediator mediator) : IInitializer
{
    public async Task Initialize(CancellationToken cancellationToken)
    {
        await mediator.Dispatch<CreateOrUpdateDefaultUsersCommand, Unit>(
            new CreateOrUpdateDefaultUsersCommand(),
            cancellationToken);
    }
}
