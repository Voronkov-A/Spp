using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Spp.Authorization.Application.Auth.Settings;
using Spp.Common.Authentication.Abstractions;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;
using Spp.IdentityProvider.Client.Applications.V1;

namespace Spp.Authorization.Application.Auth.Commands;

public class RegisterInIdentityProviderCommandHandler(
    IOptions<IdentityProviderSettings> identityProviderSettings,
    IApplicationsClient applicationsClient,
    IAuthenticationContext<ApplicationRegistrationAuthenticationContext> authenticationContext) :
    IRequestHandler<RegisterInIdentityProviderCommand, Unit>
{
    public async Task<Unit> Handle(RegisterInIdentityProviderCommand request, CancellationToken cancellationToken)
    {
        var settings = identityProviderSettings.Value;
        var application = (await applicationsClient.List(settings.ClientId, cancellationToken)).Items.SingleOrDefault();

        if (application == null)
        {
            await applicationsClient.Create(
                new CreateApplicationRequest(
                    clientId: settings.ClientId,
                    clientSecret: settings.ClientSecret,
                    redirectUris: settings.RedirectUris.ToList()),
                cancellationToken);
        }
        else if (NeedUpdate(application))
        {
            using (authenticationContext.Activate())
            {
                await applicationsClient.Update(
                    application.Id,
                    new UpdateApplicationRequest(clientSecret: settings.ClientSecret, settings.RedirectUris.ToList()),
                    cancellationToken);
            }
        }

        return default;
    }

    private bool NeedUpdate(ApplicationView existingApplication)
    {
        if (identityProviderSettings.Value.ClientSecret != identityProviderSettings.Value.OldClientSecret)
        {
            return true;
        }

        var oldRedirectUris = existingApplication.RedirectUris
            .SelectMany(x => new[] { x.ToString(), x.ToString().TrimEnd('/') })
            .ToHashSet();
        var newRedirectUris = identityProviderSettings.Value.RedirectUris
            .SelectMany(x => new[] { x.ToString(), x.ToString().TrimEnd('/') })
            .ToHashSet();
        return !oldRedirectUris.SetEquals(newRedirectUris);
    }
}
