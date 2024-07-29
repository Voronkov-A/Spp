#nullable enable

namespace Spp.IdentityProvider.WebApi.Auth.V1;


/// <summary>
/// Authorization callback parameters.
/// </summary>
public partial class AuthorizationCallbackParameters
{
    public AuthorizationCallbackParameters(
        string code,
        string scope,
        string iss
    )
    {
        this.Code = code;
        this.Scope = scope;
        this.Iss = iss;
    }

    /// <summary>
    /// Code.
    /// </summary>
    public string Code { get; }
    /// <summary>
    /// Scope.
    /// </summary>
    public string Scope { get; }
    /// <summary>
    /// Issuer.
    /// </summary>
    public string Iss { get; }
}


#nullable restore
