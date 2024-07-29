#nullable enable

namespace Spp.IdentityProvider.TestClient.Auth.V1;


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
    [System.Text.Json.Serialization.JsonPropertyName("code")]
    public string Code { get; }
    /// <summary>
    /// Scope.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("scope")]
    public string Scope { get; }
    /// <summary>
    /// Issuer.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("iss")]
    public string Iss { get; }
}


#nullable restore
