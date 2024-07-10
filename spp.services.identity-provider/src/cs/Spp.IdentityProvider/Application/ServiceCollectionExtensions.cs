using Microsoft.Extensions.DependencyInjection;
using Spp.Common.Configuration;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;
using Spp.IdentityProvider.Application.Applications.Commands;
using Spp.IdentityProvider.Application.Applications.Queries;
using Spp.IdentityProvider.Application.Applications.Settings;
using Spp.IdentityProvider.Application.Users.Commands;
using Spp.IdentityProvider.Application.Users.Settings;
using Spp.IdentityProvider.WebApi.Applications.V1;

namespace Spp.IdentityProvider.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddMediator()

            .AddSettings<DefaultApplicationSettings>("Application:DefaultApplication")
            .AddHandler<
                CreateApplicationCommand,
                CreateApplicationCommandResponse,
                CreateApplicationCommandHandler>()
            .AddHandler<
                CreateOrUpdateDefaultApplicationCommand,
                Unit,
                CreateOrUpdateDefaultApplicationCommandHandler>()
            .AddHandler<
                UpdateApplicationCommand,
                UpdateApplicationCommandResponse,
                UpdateApplicationCommandHandler>()
            .AddHandler<
                ListApplicationsQuery,
                ApplicationViewList,
                ListApplicationsQueryHandler>()

            .AddSettings<DefaultUserSetSettings>("Application:DefaultUserSet")
            .AddHandler<
                CreateOrUpdateDefaultUsersCommand,
                Unit,
                CreateOrUpdateDefaultUsersCommandHandler>()
            .AddHandler<
                CreateUserCommand,
                CreateUserCommandResponse,
                CreateUserCommandHandler>();
    }
}
