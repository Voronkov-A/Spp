using System;

namespace Spp.IdentityProvider.Domain.Users;

public readonly struct UserId(string value) : IEquatable<UserId>, IComparable<UserId>
{
    private readonly string _value = value;

    public UserId() : this(Guid.NewGuid().ToString("N"))
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
        return obj is UserId other && Equals(other);
    }

    public bool Equals(UserId other)
    {
        return _value == other._value;
    }

    public int CompareTo(UserId other)
    {
        return string.Compare(_value, other._value, StringComparison.Ordinal);
    }

    public static bool operator ==(UserId left, UserId right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(UserId left, UserId right)
    {
        return !left.Equals(right);
    }

    public static bool operator <(UserId left, UserId right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(UserId left, UserId right)
    {
        return left.CompareTo(right) > 0;
    }
}
