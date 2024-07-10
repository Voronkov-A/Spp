#nullable enable

namespace Spp.IdentityProvider.TestClient.Applications.V1;


/// <summary>
/// Create application request.
/// </summary>
public partial class CreateApplicationRequest
{
    public CreateApplicationRequest(
        string clientId,
        string clientSecret,
        System.Collections.Generic.IReadOnlyList<System.Uri> redirectUris
    )
    {
        this.ClientId = clientId;
        this.ClientSecret = clientSecret;
        this.RedirectUris = redirectUris;
    }

    /// <summary>
    /// Client identifier.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("clientId")]
    public string ClientId { get; }
    /// <summary>
    /// Client secret.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("clientSecret")]
    public string ClientSecret { get; }
    /// <summary>
    /// Allowed redirect URIs.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("redirectUris")]
    public System.Collections.Generic.IReadOnlyList<System.Uri> RedirectUris { get; }
}


#nullable restore
