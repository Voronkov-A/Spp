using Spp.Authorization.Events.Common;
using Spp.Authorization.Events.Rbac;
using Spp.Common.Subscriptions;

namespace Spp.Authorization.Client.Sdk.Persistence;

internal class AuthorizationPersistenceEventSubscription : EventSubscription
{
    public AuthorizationPersistenceEventSubscription()
    {
        When<RoleCreated>(AggregateTypes.Role)
            .Dispatch(evt => new UpdateAuthorizationPersistenceCommand<RoleCreated>(evt));
        When<RoleDeleted>(AggregateTypes.Role)
            .Dispatch(evt => new UpdateAuthorizationPersistenceCommand<RoleDeleted>(evt));
    }
}
