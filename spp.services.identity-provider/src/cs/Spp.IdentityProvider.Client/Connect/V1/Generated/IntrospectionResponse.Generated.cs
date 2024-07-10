#nullable enable

namespace Spp.IdentityProvider.Client.Connect.V1;


/// <summary>
/// Introspection response.
/// </summary>
public partial class IntrospectionResponse
{
    public IntrospectionResponse(
        bool active,
        string? sub
    )
    {
        this.Active = active;
        this.Sub = sub;
    }

    /// <summary>
    /// Either the token is active or inactive.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("active")]
    public bool Active { get; }
    /// <summary>
    /// Subject.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("sub")]
    public string? Sub { get; }
}


#nullable restore
