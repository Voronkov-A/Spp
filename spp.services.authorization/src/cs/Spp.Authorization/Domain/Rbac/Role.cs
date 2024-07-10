using System.Collections.Generic;
using System.Linq;
using Spp.Common.Domain;

namespace Spp.Authorization.Domain.Rbac;

public class Role : BaseAggregate
{
    private readonly HashSet<PermissionReference> _permissions = new();

    public Role(EntityId id, RoleName name, bool isDefault, IEnumerable<PermissionReference> permissions) :
        base(id, EventDispatcher<Role>.Instance)
    {
        AddEvent(RoleEvents.RoleCreated(name, isDefault, permissions.Distinct()));
    }

    private Role(EntityId id) : base(id, EventDispatcher<Role>.Instance)
    {
    }

    public RoleName Name { get; private set; }

    public bool IsDefault { get; private set; }

    public bool IsDeleted { get; private set; }

    public IReadOnlyCollection<PermissionReference> Permissions => _permissions;

    public void Delete()
    {
        if (IsDeleted)
        {
            return;
        }

        AddEvent(RoleEvents.RoleDeleted());
    }

    private void When(Events.Rbac.RoleCreated evt)
    {
        foreach (var permission in evt.Permissions)
        {
            _permissions.Add(new PermissionReference(
                permissionGroupId: new EntityId(permission.PermissionGroupId),
                permissionId: new EntityId(permission.PermissionId)));
        }

        Name = new RoleName(
            def: evt.Name.Default,
            translations: evt.Name.Translations
                .Select(x => new Translation(language: x.Language, value: x.Value))
                .ToList());
        IsDefault = evt.IsDefault;
    }

    private void When(Events.Rbac.RoleDeleted evt)
    {
        IsDeleted = true;
    }
}
