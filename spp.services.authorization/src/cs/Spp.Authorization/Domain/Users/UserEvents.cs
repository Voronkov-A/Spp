using System.Collections.Generic;
using System.Linq;
using Spp.Authorization.Domain.Rbac;
using Spp.Authorization.Events.Users;
using Spp.Common.Domain;

namespace Spp.Authorization.Domain.Users;

public static class UserEvents
{
    public static UserCreated UserCreated(
        UserName name,
        bool isSuperuser,
        IEnumerable<UserIdentity> identities,
        IEnumerable<Role> roles)
    {
        var identityDtos = identities
            .Select(x => new Events.Users.UserIdentity(providerId: x.ProviderId, subjectId: x.SubjectId));
        var roleIds = roles.Select(x => x.Id.ToString());
        return new UserCreated(
            name: name.ToString(),
            isSuperuser: isSuperuser,
            identities: identityDtos.ToList(),
            roleIds: roleIds.ToList());
    }

    public static UserRenamed UserRenamed(UserName name)
    {
        return new UserRenamed(name: name.ToString());
    }

    public static UserBlocked UserBlocked()
    {
        return new UserBlocked();
    }

    public static UserUnblocked UserUnblocked()
    {
        return new UserUnblocked();
    }

    public static UserRoleAssigned UserRoleAssigned(Role role)
    {
        return new UserRoleAssigned(role.Id.ToString());
    }

    public static UserRoleUnassigned UserRoleUnassigned(Role role)
    {
        return new UserRoleUnassigned(role.Id.ToString());
    }
}
