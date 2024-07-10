#nullable enable

namespace Spp.IdentityProvider.TestClient.Connect.V1;


/// <summary>
/// The OpenID configuration document as defined by the specification: http://openid.net/specs/openid-connect-discovery-1_0.html#ProviderMetadata. 
/// </summary>
public partial class DiscoveryDocumentResponse
{
    public DiscoveryDocumentResponse(
        string? userinfoEndpoint,
        string jwksUri,
        System.Collections.Generic.IReadOnlyList<string>? scopesSupported,
        System.Collections.Generic.IReadOnlyList<string> subjectTypesSupported,
        string tokenEndpoint,
        System.Collections.Generic.IReadOnlyList<string> idTokenSigningAlgValuesSupported,
        System.Collections.Generic.IReadOnlyList<string> responseTypesSupported,
        System.Collections.Generic.IReadOnlyList<string>? claimsSupported,
        string authorizationEndpoint,
        string issuer,
        System.Collections.Generic.IReadOnlyList<string>? grantTypesSupported,
        System.Collections.Generic.IReadOnlyList<string>? acrValuesSupported,
        System.Collections.Generic.IReadOnlyList<string>? tokenEndpointAuthMethodsSupported,
        System.Collections.Generic.IReadOnlyList<string>? tokenEndpointAuthSigningAlgValuesSupported,
        System.Collections.Generic.IReadOnlyList<string>? displayValuesSupported,
        System.Collections.Generic.IReadOnlyList<string>? claimTypesSupported,
        string? serviceDocumentation,
        System.Collections.Generic.IReadOnlyList<string>? uiLocalesSupported
    )
    {
        this.UserinfoEndpoint = userinfoEndpoint;
        this.JwksUri = jwksUri;
        this.ScopesSupported = scopesSupported;
        this.SubjectTypesSupported = subjectTypesSupported;
        this.TokenEndpoint = tokenEndpoint;
        this.IdTokenSigningAlgValuesSupported = idTokenSigningAlgValuesSupported;
        this.ResponseTypesSupported = responseTypesSupported;
        this.ClaimsSupported = claimsSupported;
        this.AuthorizationEndpoint = authorizationEndpoint;
        this.Issuer = issuer;
        this.GrantTypesSupported = grantTypesSupported;
        this.AcrValuesSupported = acrValuesSupported;
        this.TokenEndpointAuthMethodsSupported = tokenEndpointAuthMethodsSupported;
        this.TokenEndpointAuthSigningAlgValuesSupported = tokenEndpointAuthSigningAlgValuesSupported;
        this.DisplayValuesSupported = displayValuesSupported;
        this.ClaimTypesSupported = claimTypesSupported;
        this.ServiceDocumentation = serviceDocumentation;
        this.UiLocalesSupported = uiLocalesSupported;
    }

    /// <summary>
    /// URL of the user info endpoint.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("userinfo_endpoint")]
    public string? UserinfoEndpoint { get; }
    /// <summary>
    /// URL of JSON Web Key Set document.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("jwks_uri")]
    public string JwksUri { get; }
    /// <summary>
    /// List of the OAuth 2.0 scope values that this server supports.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("scopes_supported")]
    public System.Collections.Generic.IReadOnlyList<string>? ScopesSupported { get; }
    /// <summary>
    /// List of the Subject Identifier types that this OP supports.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("subject_types_supported")]
    public System.Collections.Generic.IReadOnlyList<string> SubjectTypesSupported { get; }
    /// <summary>
    /// URL of the OAuth 2.0 token endpoint.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("token_endpoint")]
    public string TokenEndpoint { get; }
    /// <summary>
    /// List of the JWS signing algorithms (alg values) supported by the OP for the ID Token to encode the Claims in a JWT. 
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("id_token_signing_alg_values_supported")]
    public System.Collections.Generic.IReadOnlyList<string> IdTokenSigningAlgValuesSupported { get; }
    /// <summary>
    /// List of the OAuth 2.0 response_type values that this OP supports.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("response_types_supported")]
    public System.Collections.Generic.IReadOnlyList<string> ResponseTypesSupported { get; }
    /// <summary>
    /// List of the Claim Names of the Claims that the OpenID Provider MAY be able to supply values for. 
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("claims_supported")]
    public System.Collections.Generic.IReadOnlyList<string>? ClaimsSupported { get; }
    /// <summary>
    /// URL of the OAuth 2.0 authorization endpoint.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("authorization_endpoint")]
    public string AuthorizationEndpoint { get; }
    /// <summary>
    /// The identifier of the token&#39;s issuer. This is identical to the &#39;iss&#39; Claim value in ID Tokens. 
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("issuer")]
    public string Issuer { get; }
    /// <summary>
    /// List of the OAuth 2.0 Grant Type values that this OP supports.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("grant_types_supported")]
    public System.Collections.Generic.IReadOnlyList<string>? GrantTypesSupported { get; }
    /// <summary>
    /// List of the Authentication Context Class References that this OP supports.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("acr_values_supported")]
    public System.Collections.Generic.IReadOnlyList<string>? AcrValuesSupported { get; }
    /// <summary>
    /// List of Client Authentication methods supported by this Token Endpoint.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("token_endpoint_auth_methods_supported")]
    public System.Collections.Generic.IReadOnlyList<string>? TokenEndpointAuthMethodsSupported { get; }
    /// <summary>
    /// List of the JWS signing algorithms (alg values) supported by the Token Endpoint for the signature on the JWT used to authenticate the Client at the Token Endpoint for the private_key_jwt and client_secret_jwt authentication methods. Servers SHOULD support RS256. The value none MUST NOT be used. 
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("token_endpoint_auth_signing_alg_values_supported")]
    public System.Collections.Generic.IReadOnlyList<string>? TokenEndpointAuthSigningAlgValuesSupported { get; }
    /// <summary>
    /// List of the display parameter values that the OpenID Provider supports.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("display_values_supported")]
    public System.Collections.Generic.IReadOnlyList<string>? DisplayValuesSupported { get; }
    /// <summary>
    /// List of the Claim Types that the OpenID Provider supports.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("claim_types_supported")]
    public System.Collections.Generic.IReadOnlyList<string>? ClaimTypesSupported { get; }
    /// <summary>
    /// URL of a page containing human-readable information that developers might want or need to know when using the OpenID Provider. 
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("service_documentation")]
    public string? ServiceDocumentation { get; }
    /// <summary>
    /// Languages and scripts supported for the user interface.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("ui_locales_supported")]
    public System.Collections.Generic.IReadOnlyList<string>? UiLocalesSupported { get; }
}


#nullable restore
