using System;
using Spp.Authorization.Domain.Common.Exceptions;

namespace Spp.Authorization.Domain.Users;

public readonly struct UserName : IEquatable<UserName>
{
    public const string GeneratedPrefix = "* ";

    private readonly string _value;

    public UserName() : this(Guid.NewGuid().ToString("N"))
    {
    }

    internal UserName(string value)
    {
        if (value.Length > 1024 || string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidNameException($"Name does not match the requirements.", value);
        }

        _value = value;
    }

    public bool IsGenerated => _value.StartsWith(GeneratedPrefix);

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
        return obj is UserName other && Equals(other);
    }

    public bool Equals(UserName other)
    {
        return _value == other._value;
    }

    public static UserName CreateCustom(string value)
    {
        if (value.StartsWith(GeneratedPrefix))
        {
            throw new InvalidNameException($"Name does not match the requirements.", value);
        }

        return new UserName(value);
    }

    public static UserName CreateGenerated(string value)
    {
        if (!value.StartsWith(GeneratedPrefix))
        {
            throw new InvalidOperationException(
                $"Generated name must start with generated prefix. Actual value is '{value}'.");
        }

        return new UserName(value);
    }

    public static bool operator ==(UserName left, UserName right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(UserName left, UserName right)
    {
        return !left.Equals(right);
    }
}
