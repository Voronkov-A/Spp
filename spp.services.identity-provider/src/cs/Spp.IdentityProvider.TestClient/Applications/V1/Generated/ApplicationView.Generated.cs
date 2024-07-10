#nullable enable

namespace Spp.IdentityProvider.TestClient.Applications.V1;


/// <summary>
/// Application.
/// </summary>
public partial class ApplicationView
{
    public ApplicationView(
        string id,
        string clientId,
        System.Collections.Generic.IReadOnlyList<System.Uri> redirectUris
    )
    {
        this.Id = id;
        this.ClientId = clientId;
        this.RedirectUris = redirectUris;
    }

    /// <summary>
    /// Application identifier.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    public string Id { get; }
    /// <summary>
    /// Client identifier.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("clientId")]
    public string ClientId { get; }
    /// <summary>
    /// Allowed redirect URIs.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("redirectUris")]
    public System.Collections.Generic.IReadOnlyList<System.Uri> RedirectUris { get; }
}


#nullable restore
