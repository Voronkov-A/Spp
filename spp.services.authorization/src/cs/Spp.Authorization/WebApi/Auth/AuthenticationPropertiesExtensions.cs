using Microsoft.AspNetCore.Authentication;
using System;

namespace Spp.Authorization.WebApi.Auth;

public static class AuthenticationPropertiesExtensions
{
    public static void SetScope(this AuthenticationProperties properties, string scope)
    {
        properties.Items[AuthenticationPropertyKeys.Scope] = scope;
    }

    public static void SetProviderId(this AuthenticationProperties properties, string providerId)
    {
        properties.Items[AuthenticationPropertyKeys.ProviderId] = providerId;
    }

    public static string GetProviderId(this AuthenticationProperties properties)
    {
        return properties.Items[AuthenticationPropertyKeys.ProviderId]
            ?? throw new InvalidOperationException($"Could not find item '{AuthenticationPropertyKeys.ProviderId}'.");
    }
}
