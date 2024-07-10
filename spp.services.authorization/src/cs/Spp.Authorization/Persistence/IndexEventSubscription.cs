using Spp.Authorization.Domain.GeneratedUserNameParts;
using Spp.Authorization.Domain.Rbac;
using Spp.Authorization.Domain.Users;
using Spp.Authorization.Events.GeneratedUserNameParts;
using Spp.Authorization.Events.Rbac;
using Spp.Authorization.Events.Users;
using Spp.Common.Cqs;
using Spp.Common.Subscriptions;

namespace Spp.Authorization.Persistence;

public class IndexEventSubscription : EventSubscription
{
    public IndexEventSubscription()
    {
        When<GeneratedUserNamePartCreated>(nameof(GeneratedUserNamePart)).Dispatch(UpdateIndexCommand.Create);
        When<GeneratedUserNamePartDeleted>(nameof(GeneratedUserNamePart)).Dispatch(UpdateIndexCommand.Create);

        When<RoleCreated>(nameof(Role)).Dispatch(UpdateIndexCommand.Create);
        When<RoleDeleted>(nameof(Role)).Dispatch(UpdateIndexCommand.Create);

        When<UserCreated>(nameof(User)).Dispatch(UpdateIndexCommand.Create);
        When<UserRenamed>(nameof(User)).Dispatch(UpdateIndexCommand.Create);
    }
}
