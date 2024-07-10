using Microsoft.Extensions.DependencyInjection;
using Spp.Authorization.Application.Auth.Commands;
using Spp.Authorization.Application.Rbac.Commands;
using Spp.Authorization.Application.Users.Commands;
using Spp.Authorization.Application.Users.Models;
using Spp.Authorization.Application.Users.Settings;
using Spp.Common.Configuration;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;
using Spp.Common.Transactions;

namespace Spp.Authorization.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddMediator()
            .AddUnitOfWork()

            .AddHandler<RegisterInIdentityProviderCommand, Unit, RegisterInIdentityProviderCommandHandler>()

            .AddHandler<CreateOrUpdatePermissionGroupCommand, Unit, CreateOrUpdatePermissionGroupCommandHandler>()
            .AddHandler<CreateRoleCommand, CreateRoleCommandResponse, CreateRoleCommandHandler>()
            .AddHandler<DeleteRoleCommand, DeleteRoleCommandResponse, DeleteRoleCommandHandler>()

            .AddSettings<SuperuserSetSettings>("Application:SuperuserSet")
            .AddHandler<AssignUserRoleCommand, AssignUserRoleCommandResponse, AssignUserRoleCommandHandler>()
            .AddHandler<CreateOrUpdateSuperusersCommand, Unit, CreateOrUpdateSuperusersCommandHandler>()
            .AddHandler<
                CreateUserIfNotExistsCommand,
                CreateUserIfNotExistsResponse,
                CreateUserIfNotExistsCommandHandler>()
            .AddHandler<UnassignUserRoleCommand, UnassignUserRoleCommandResponse, UnassignUserRoleCommandHandler>();
    }
}
