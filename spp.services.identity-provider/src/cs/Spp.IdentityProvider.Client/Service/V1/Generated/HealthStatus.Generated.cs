#nullable enable

namespace Spp.IdentityProvider.Client.Service.V1;

/// <summary>
/// Service health status.
/// </summary>
public enum HealthStatus
{
    [System.Runtime.Serialization.EnumMember(Value = "unhealthy")]
    Unhealthy = 0,
    [System.Runtime.Serialization.EnumMember(Value = "degraded")]
    Degraded,
    [System.Runtime.Serialization.EnumMember(Value = "healthy")]
    Healthy
}



#nullable restore
