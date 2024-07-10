using Spp.Authorization.Application.Users.Models;
using Spp.Authorization.Domain.Users;
using Spp.Common.Cqs;

namespace Spp.Authorization.Application.Users.Commands;

public readonly struct CreateUserIfNotExistsCommand(UserIdentity identity) :
    ICommand<CreateUserIfNotExistsResponse>, IRequiresUnitOfWork
{
    public UserIdentity Identity { get; } = identity;
}
