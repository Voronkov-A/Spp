using Spp.IdentityProvider.Application.Applications.Commands;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Initialization;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;

namespace Spp.IdentityProvider.Initialization;

public class DefaultApplicationInitializer(IMediator mediator) : IInitializer
{
    public async Task Initialize(CancellationToken cancellationToken)
    {
        await mediator.Dispatch<CreateOrUpdateDefaultApplicationCommand, Unit>(
            new CreateOrUpdateDefaultApplicationCommand(),
            cancellationToken);
    }
}
