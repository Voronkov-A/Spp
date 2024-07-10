#nullable enable

namespace Spp.IdentityProvider.WebApi.Applications.V1;


/// <summary>
/// Update application request.
/// </summary>
public partial class UpdateApplicationRequest
{
    public UpdateApplicationRequest(
        string clientSecret,
        System.Collections.Generic.IReadOnlyList<System.Uri> redirectUris
    )
    {
        this.ClientSecret = clientSecret;
        this.RedirectUris = redirectUris;
    }

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
