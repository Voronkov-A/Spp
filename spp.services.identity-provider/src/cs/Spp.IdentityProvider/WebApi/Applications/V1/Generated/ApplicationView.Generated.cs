#nullable enable

namespace Spp.IdentityProvider.WebApi.Applications.V1;


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
    public string Id { get; }
    /// <summary>
    /// Client identifier.
    /// </summary>
    public string ClientId { get; }
    /// <summary>
    /// Allowed redirect URIs.
    /// </summary>
    public System.Collections.Generic.IReadOnlyList<System.Uri> RedirectUris { get; }
}


#nullable restore
