using Spp.Authorization.Application.Users.Commands;
using Spp.Common.Initialization;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Initialization;

public class SuperuserInitializer(IMediator mediator) : IInitializer
{
    public async Task Initialize(CancellationToken cancellationToken)
    {
        await mediator.Dispatch<CreateOrUpdateSuperusersCommand, Unit>(
            new CreateOrUpdateSuperusersCommand(),
            cancellationToken);
    }
}
