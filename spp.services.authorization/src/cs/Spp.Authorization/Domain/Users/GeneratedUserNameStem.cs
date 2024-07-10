using System;

namespace Spp.Authorization.Domain.Users;

public readonly struct GeneratedUserNameStem : IEquatable<GeneratedUserNameStem>
{
    private readonly UserName _value;

    public GeneratedUserNameStem() : this(UserName.GeneratedPrefix)
    {
    }

    public GeneratedUserNameStem(string firstName, string lastName)
        : this($"{UserName.GeneratedPrefix}{firstName} {lastName}")
    {
    }

    private GeneratedUserNameStem(string value)
    {
        _value = UserName.CreateGenerated(value);
    }

    public override string ToString()
    {
        return _value.ToString();
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is GeneratedUserNameStem other && Equals(other);
    }

    public bool Equals(GeneratedUserNameStem other)
    {
        return _value == other._value;
    }

    public static bool operator ==(GeneratedUserNameStem left, GeneratedUserNameStem right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(GeneratedUserNameStem left, GeneratedUserNameStem right)
    {
        return !left.Equals(right);
    }
}
