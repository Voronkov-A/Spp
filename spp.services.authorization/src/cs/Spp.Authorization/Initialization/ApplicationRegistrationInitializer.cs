using System.Threading;
using System.Threading.Tasks;
using Spp.Authorization.Application.Auth.Commands;
using Spp.Common.Initialization;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;

namespace Spp.Authorization.Initialization;

public class ApplicationRegistrationInitializer(
    IRequestHandler<RegisterInIdentityProviderCommand, Unit> registerInIdentityProviderHandler) :
    IInitializer
{
    public async Task Initialize(CancellationToken cancellationToken)
    {
        await registerInIdentityProviderHandler.Handle(new RegisterInIdentityProviderCommand(), cancellationToken);
    }
}
