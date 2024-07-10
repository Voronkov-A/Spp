using Spp.Authorization.Events.Rbac;
using Spp.Common.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Spp.Authorization.Domain.Rbac;

public class PermissionGroup : BaseAggregate
{
    private readonly List<Permission> _permissions = new();

    public PermissionGroup(EntityId id, IEnumerable<Permission> permissions)
        : base(id, EventDispatcher<PermissionGroup>.Instance)
    {
        AddEvent(new PermissionGroupCreated(
            permissionIds: permissions.Order(PermissionComparer.Instance).Select(x => x.Id.ToString()).ToList()));
    }

    private PermissionGroup(EntityId id) : base(id, EventDispatcher<PermissionGroup>.Instance)
    {
    }

    public Permission? FindPermission(EntityId id)
    {
        return _permissions.Cast<Permission?>().FirstOrDefault(x => x?.Id == id);
    }

    public void Update(IEnumerable<Permission> permissions)
    {
        var permissionList = permissions.Order(PermissionComparer.Instance).ToList();
        var addedPermissionIds = permissionList
            .Where(x => _permissions.All(existing => existing.Id != x.Id))
            .Select(x => x.Id.ToString())
            .ToList();
        var removedPermissionIds = _permissions
            .Where(existing => _permissions.All(x => x.Id != existing.Id))
            .Select(x => x.Id.ToString())
            .ToList();

        if (addedPermissionIds.Count == 0 && removedPermissionIds.Count == 0)
        {
            return;
        }

        AddEvent(new PermissionGroupUpdated(
            addedPermissionIds: addedPermissionIds,
            removedPermissionIds: removedPermissionIds));
    }

    private void When(PermissionGroupCreated evt)
    {
        var permissions = evt.PermissionIds.Select(x => new Permission(new EntityId(x)));
        _permissions.AddRange(permissions);
    }

    private void When(PermissionGroupUpdated evt)
    {
        _permissions.RemoveAll(x => evt.RemovedPermissionIds.Contains(x.Id.ToString()));
        var addedPermissions = evt.AddedPermissionIds.Select(x => new Permission(new EntityId(x)));
        _permissions.AddRange(addedPermissions);
        _permissions.Sort(PermissionComparer.Instance);
    }

    private class PermissionComparer : IComparer<Permission>
    {
        public static readonly PermissionComparer Instance = new();

        public int Compare(Permission x, Permission y)
        {
            return x.Id.CompareTo(y.Id);
        }
    }
}
