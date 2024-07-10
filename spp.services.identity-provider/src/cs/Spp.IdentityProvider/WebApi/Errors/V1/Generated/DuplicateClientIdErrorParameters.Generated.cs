#nullable enable

namespace Spp.IdentityProvider.WebApi.Errors.V1;


/// <summary>
/// Parameters for identityProvider.duplicateClientId error code.
/// </summary>
public partial class DuplicateClientIdErrorParameters
{
    public DuplicateClientIdErrorParameters(
        string clientId
    )
    {
        this.ClientId = clientId;
    }

    /// <summary>
    /// Client id.
    /// </summary>
    public string ClientId { get; }
}


#nullable restore
