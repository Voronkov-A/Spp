using System;

namespace Spp.Common.Authentication.Abstractions;

public class AccessToken(string value, DateTimeOffset expirationTime)
{
    public string Value { get; } = value;

    public DateTimeOffset ExpirationTime { get; } = expirationTime;
}
