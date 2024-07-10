using System;
using Spp.Common.Domain;

namespace Spp.Authorization.Domain.Rbac;

public readonly struct PermissionReference(EntityId permissionGroupId, EntityId permissionId) :
    IEquatable<PermissionReference>
{
    public EntityId PermissionGroupId { get; } = permissionGroupId;

    public EntityId PermissionId { get; } = permissionId;

    public override int GetHashCode()
    {
        return HashCode.Combine(PermissionGroupId, PermissionId);
    }

    public override bool Equals(object? obj)
    {
        return obj is PermissionReference other && Equals(other);
    }

    public bool Equals(PermissionReference other)
    {
        return PermissionGroupId == other.PermissionGroupId && PermissionId == other.PermissionId;
    }

    public override string ToString()
    {
        return $"{PermissionGroupId}/{PermissionId}";
    }

    public static bool operator ==(PermissionReference left, PermissionReference right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(PermissionReference left, PermissionReference right)
    {
        return !left.Equals(right);
    }
}
