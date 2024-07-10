#nullable enable

namespace Spp.IdentityProvider.WebApi.Applications.V1;


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
    public string ClientId { get; }
    /// <summary>
    /// Client secret.
    /// </summary>
    public string ClientSecret { get; }
    /// <summary>
    /// Allowed redirect URIs.
    /// </summary>
    public System.Collections.Generic.IReadOnlyList<System.Uri> RedirectUris { get; }
}


#nullable restore
