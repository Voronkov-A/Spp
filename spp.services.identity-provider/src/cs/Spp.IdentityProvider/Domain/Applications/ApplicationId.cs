using System;

namespace Spp.IdentityProvider.Domain.Applications;

public readonly struct ApplicationId(string value) : IEquatable<ApplicationId>, IComparable<ApplicationId>
{
    private readonly string _value = value;

    public ApplicationId() : this(Guid.NewGuid().ToString("N"))
    {
    }

    public override string ToString()
    {
        return _value;
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is ApplicationId other && Equals(other);
    }

    public bool Equals(ApplicationId other)
    {
        return _value == other._value;
    }

    public int CompareTo(ApplicationId other)
    {
        return string.Compare(_value, other._value, StringComparison.Ordinal);
    }

    public static bool operator ==(ApplicationId left, ApplicationId right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ApplicationId left, ApplicationId right)
    {
        return !left.Equals(right);
    }

    public static bool operator <(ApplicationId left, ApplicationId right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(ApplicationId left, ApplicationId right)
    {
        return left.CompareTo(right) > 0;
    }
}
