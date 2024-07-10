using Spp.Authorization.Domain.Common.Exceptions;
using Spp.Authorization.Domain.Users.Exceptions;
using System.Collections.Generic;
using System.Linq;
using Spp.Authorization.Domain.Rbac;
using Spp.Common.Domain;

namespace Spp.Authorization.Domain.Users;

public class User : BaseAggregate
{
    private readonly List<UserIdentity> _identities = new();
    private readonly List<EntityId> _roles = new();

    public User(
        EntityId id,
        UserName name,
        bool isSuperuser,
        IEnumerable<UserIdentity> identities,
        IEnumerable<Role> roles) :
        base(id, EventDispatcher<User>.Instance)
    {
        AddEvent(UserEvents.UserCreated(name, isSuperuser, identities.Distinct(), roles.DistinctBy(x => x.Id)));
    }

    public User(EntityId id, UserName name, bool isSuperuser, UserIdentity identity, IEnumerable<Role> roles) :
        this(id, name, isSuperuser, new[] { identity }, roles)
    {
    }

    public User(EntityId id, UserName name, bool isSuperuser, IEnumerable<UserIdentity> identities) :
        this(id, name, isSuperuser, identities, Enumerable.Empty<Role>())
    {
    }

    private User(EntityId id) : base(id, EventDispatcher<User>.Instance)
    {
    }

    public UserName Name { get; private set; }

    public IReadOnlyCollection<UserIdentity> Identities => _identities;

    public IReadOnlyCollection<EntityId> Roles => _roles;

    public bool IsSuperuser { get; private set; }

    public bool IsBlocked { get; private set; }

    public bool HasIdentity(UserIdentity identity)
    {
        return _identities.Contains(identity);
    }

    public void AddIdentities(IEnumerable<UserIdentity> identities)
    {
        throw new System.NotImplementedException();
    }

    public void RemoveIdentity(UserIdentity identity)
    {
        throw new System.NotImplementedException();
    }

    public void Rename(UserName name)
    {
        if (name == Name)
        {
            return;
        }

        if (name.IsGenerated)
        {
            throw new InvalidNameException($"Name does not match the requirements.", name.ToString());
        }

        AddEvent(UserEvents.UserRenamed(name));
    }

    public void Block()
    {
        if (IsBlocked)
        {
            return;
        }

        if (IsSuperuser)
        {
            throw new CannotBlockUserException("Cannot block superuser.", Id);
        }

        AddEvent(UserEvents.UserBlocked());
    }

    public void Unblock()
    {
        if (!IsBlocked)
        {
            return;
        }

        AddEvent(UserEvents.UserUnblocked());
    }

    public void Assign(Role role)
    {
        if (_roles.Contains(role.Id))
        {
            return;
        }

        if (IsSuperuser)
        {
            throw new CannotAssignRoleException("Cannot assign role to a superuser.", Id);
        }

        AddEvent(UserEvents.UserRoleAssigned(role));
    }

    public void Unassign(Role role)
    {
        if (!_roles.Contains(role.Id))
        {
            return;
        }

        AddEvent(UserEvents.UserRoleUnassigned(role));
    }

    private void When(Events.Users.UserCreated evt)
    {
        Name = new UserName(evt.Name);
        IsSuperuser = evt.IsSuperuser;

        var identities = evt.Identities.Select(x => new UserIdentity(providerId: x.ProviderId, subjectId: x.SubjectId));
        _identities.AddRange(identities);

        var roles = evt.RoleIds.Select(x => new EntityId(x));
        _roles.AddRange(roles);
    }

    private void When(Events.Users.UserRenamed evt)
    {
        Name = new UserName(evt.Name);
    }

    private void When(Events.Users.UserBlocked _)
    {
        IsBlocked = true;
    }

    private void When(Events.Users.UserUnblocked _)
    {
        IsBlocked = false;
    }

    private void When(Events.Users.UserRoleAssigned evt)
    {
        _roles.Add(new EntityId(evt.RoleId));
    }

    private void When(Events.Users.UserRoleUnassigned evt)
    {
        _roles.Remove(new EntityId(evt.RoleId));
    }
}
