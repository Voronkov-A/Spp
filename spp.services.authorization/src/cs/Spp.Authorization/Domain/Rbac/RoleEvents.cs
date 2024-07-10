using Spp.Authorization.Events.Rbac;
using System.Collections.Generic;
using System.Linq;

namespace Spp.Authorization.Domain.Rbac;

public static class RoleEvents
{
    public static RoleCreated RoleCreated(RoleName name, bool isDefault, IEnumerable<PermissionReference> permissions)
    {
        return new RoleCreated(
            name: new LocalizedName(
                @default: name.Default,
                translations: name.Translations
                    .Select(x => new Events.Rbac.Translation(language: x.Language, value: x.Value))
                    .ToList()),
            isDefault: isDefault,
            permissions: permissions
                .Select(x => new Events.Rbac.PermissionReference(
                    permissionGroupId: x.PermissionGroupId.ToString(),
                    permissionId: x.PermissionId.ToString()))
                .ToList());
    }

    public static RoleDeleted RoleDeleted()
    {
        return new RoleDeleted();
    }
}
