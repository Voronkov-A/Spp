using System;
using System.Diagnostics.CodeAnalysis;

namespace Spp.Authorization.Domain.Users;

public readonly struct UserIdentity(string providerId, string subjectId) : IEquatable<UserIdentity>
{
    public string ProviderId { get; } = providerId;

    public string SubjectId { get; } = subjectId;

    public bool Equals(UserIdentity other)
    {
        return ProviderId == other.ProviderId && SubjectId == other.SubjectId;
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is UserIdentity other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ProviderId, SubjectId);
    }

    public static bool operator ==(UserIdentity left, UserIdentity right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(UserIdentity left, UserIdentity right)
    {
        return !left.Equals(right);
    }
}
