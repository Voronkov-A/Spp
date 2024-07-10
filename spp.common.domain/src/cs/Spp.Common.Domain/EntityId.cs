using System;

namespace Spp.Common.Domain;

public readonly struct EntityId(string value) : IEquatable<EntityId>, IComparable<EntityId>
{
    private readonly string _value = value;

    public EntityId() : this(Guid.NewGuid().ToString("N"))
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
        return obj is EntityId other && Equals(other);
    }

    public bool Equals(EntityId other)
    {
        return _value == other._value;
    }

    public int CompareTo(EntityId other)
    {
        return string.Compare(_value, other._value, StringComparison.Ordinal);
    }

    public static bool operator ==(EntityId left, EntityId right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(EntityId left, EntityId right)
    {
        return !left.Equals(right);
    }

    public static bool operator <(EntityId left, EntityId right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(EntityId left, EntityId right)
    {
        return left.CompareTo(right) > 0;
    }
}
