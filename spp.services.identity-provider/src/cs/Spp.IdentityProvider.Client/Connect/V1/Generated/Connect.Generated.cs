#nullable enable

namespace Spp.IdentityProvider.Client.Connect.V1;

public partial class ConnectClient : IConnectClient, System.IDisposable
{
    private static readonly System.Net.Http.HttpMethod DELETE = System.Net.Http.HttpMethod.Delete;
    private static readonly System.Net.Http.HttpMethod GET = System.Net.Http.HttpMethod.Get;
    private static readonly System.Net.Http.HttpMethod POST = System.Net.Http.HttpMethod.Post;
    private static readonly System.Net.Http.HttpMethod PUT = System.Net.Http.HttpMethod.Put;

    private readonly System.Net.Http.HttpClient _httpClient;
    private readonly System.Text.Json.JsonSerializerOptions _jsonSerializerOptions;

    public ConnectClient(System.Net.Http.HttpClient httpClient, Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Http.Json.JsonOptions> jsonOptions)
    {
        _httpClient = httpClient;
        _jsonSerializerOptions = jsonOptions.Value.SerializerOptions;
    }

    public void Dispose()
    {
        Dispose(true);
        System.GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Initializes an OAuth 2.0 flow using a response_type. See RFC 6749 (https://tools.ietf.org/html/rfc6749) for more details. 
    /// </summary>
    public async System.Threading.Tasks.Task Authorize(
        string clientId,
        ResponseType responseType,
        string redirectUri,
        string scope,
        string? codeChallenge,
        CodeChallengeMethod? codeChallengeMethod,
        ResponseMode? responseMode,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = Authorize_CreateRequest(
            clientId,
            responseType,
            redirectUri,
            scope,
            codeChallenge,
            codeChallengeMethod,
            responseMode
        );

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode != 302)
        {
            throw new System.InvalidOperationException("Expected status code 302, but the server responded with " + response.StatusCode.ToString() + ".");
        }

    }

    /// <summary>
    /// The end session endpoint can be used to trigger single sign-out in the browser.
    /// </summary>
    public async System.Threading.Tasks.Task EndSession(
        string? idTokenHint,
        string? postLogoutRedirectUri,
        string? state,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = EndSession_CreateRequest(
            idTokenHint,
            postLogoutRedirectUri,
            state
        );

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode != 204)
        {
            throw new System.InvalidOperationException("Expected status code 204, but the server responded with " + response.StatusCode.ToString() + ".");
        }

    }

    /// <summary>
    /// This endpoint follows the specification defined at http://openid.net/specs/openid-connect-discovery-1_0.html#ProviderMetadata. It provides a mechanism for an OpenID Connect Relying Party to discover the End-User&#39;s OpenID Provider and obtain information needed to interact with it, including its OAuth 2.0 endpoint locations. 
    /// </summary>
    public async System.Threading.Tasks.Task<DiscoveryDocumentResponse> GetDiscoveryDocument(
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = GetDiscoveryDocument_CreateRequest(
        );

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode != 200)
        {
            throw new System.InvalidOperationException("Expected status code 200, but the server responded with " + response.StatusCode.ToString() + ".");
        }

        var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<DiscoveryDocumentResponse?>(
            response.Content,
            _jsonSerializerOptions,
            cancellationToken);
        return content ?? throw new System.InvalidOperationException("Code is 200, but result is null.");
    }

    /// <summary>
    /// This API reflects an implementation according to OpenID Connect. This endpoint returns a message with user details. The content depends on the SCOPE associated with the given access_token. IMPORTANT: The API will fail if no active id_token is available for the associated user. For more information refer to this website: http://openid.net/specs/openid-connect-core-1_0.html#UserInfo. 
    /// </summary>
    public async System.Threading.Tasks.Task<UserInfo> GetUserInfo(
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = GetUserInfo_CreateRequest(
        );

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode != 200)
        {
            throw new System.InvalidOperationException("Expected status code 200, but the server responded with " + response.StatusCode.ToString() + ".");
        }

        var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<UserInfo?>(
            response.Content,
            _jsonSerializerOptions,
            cancellationToken);
        return content ?? throw new System.InvalidOperationException("Code is 200, but result is null.");
    }

    /// <summary>
    /// The introspection endpoint is an implementation of RFC 7662.
    /// </summary>
    public async System.Threading.Tasks.Task<IntrospectionResponse> IntrospectToken(
        string token,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = IntrospectToken_CreateRequest(
            token
        );

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode != 200)
        {
            throw new System.InvalidOperationException("Expected status code 200, but the server responded with " + response.StatusCode.ToString() + ".");
        }

        var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<IntrospectionResponse?>(
            response.Content,
            _jsonSerializerOptions,
            cancellationToken);
        return content ?? throw new System.InvalidOperationException("Code is 200, but result is null.");
    }

    /// <summary>
    /// Request access_token or refresh_token using OAuth 2.0 grant_type. See RFC 6749 for more details. 
    /// </summary>
    public async System.Threading.Tasks.Task<TokenData> RequestAuthorizationCodeToken(
        string clientId,
        GrantType grantType,
        string? clientSecret,
        string? scope,
        string? redirectUri,
        string? code,
        string? codeVerifier,
        string? refreshToken,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = RequestAuthorizationCodeToken_CreateRequest(
            clientId,
            grantType,
            clientSecret,
            scope,
            redirectUri,
            code,
            codeVerifier,
            refreshToken
        );

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode != 200)
        {
            throw new System.InvalidOperationException("Expected status code 200, but the server responded with " + response.StatusCode.ToString() + ".");
        }

        var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<TokenData?>(
            response.Content,
            _jsonSerializerOptions,
            cancellationToken);
        return content ?? throw new System.InvalidOperationException("Code is 200, but result is null.");
    }

    /// <summary>
    /// This endpoint allows revoking access tokens (reference tokens only) and refresh token. It implements the token revocation specification (RFC 7009). 
    /// </summary>
    public async System.Threading.Tasks.Task RevokeToken(
        string token,
        TokenType? tokenTypeHint,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = RevokeToken_CreateRequest(
            token,
            tokenTypeHint
        );

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode != 204)
        {
            throw new System.InvalidOperationException("Expected status code 204, but the server responded with " + response.StatusCode.ToString() + ".");
        }

    }


    private System.Net.Http.HttpRequestMessage Authorize_CreateRequest(
        string clientId,
        ResponseType responseType,
        string redirectUri,
        string scope,
        string? codeChallenge,
        CodeChallengeMethod? codeChallengeMethod,
        ResponseMode? responseMode
    )
    {
        var path = "/connect/authorize";


        var uriBuilder = new System.Text.StringBuilder(path);

        var separator = '?';

        if (true)
        {
            uriBuilder
                .Append(separator)
                .Append("client_id=")
                .Append(UrlEncode(ValueToString(clientId)));
            separator = '&';
        }
        if (true)
        {
            uriBuilder
                .Append(separator)
                .Append("response_type=")
                .Append(UrlEncode(EnumToString(responseType)));
            separator = '&';
        }
        if (true)
        {
            uriBuilder
                .Append(separator)
                .Append("redirect_uri=")
                .Append(UrlEncode(ValueToString(redirectUri)));
            separator = '&';
        }
        if (true)
        {
            uriBuilder
                .Append(separator)
                .Append("scope=")
                .Append(UrlEncode(ValueToString(scope)));
            separator = '&';
        }
        if (true && codeChallenge != null)
        {
            uriBuilder
                .Append(separator)
                .Append("code_challenge=")
                .Append(UrlEncode(ValueToString(codeChallenge ?? throw new System.InvalidOperationException("Unexpected null value."))));
            separator = '&';
        }
        if (true && codeChallengeMethod != null)
        {
            uriBuilder
                .Append(separator)
                .Append("code_challenge_method=")
                .Append(UrlEncode(EnumToString(codeChallengeMethod ?? throw new System.InvalidOperationException("Unexpected null value."))));
            separator = '&';
        }
        if (true && responseMode != null)
        {
            uriBuilder
                .Append(separator)
                .Append("response_mode=")
                .Append(UrlEncode(EnumToString(responseMode ?? throw new System.InvalidOperationException("Unexpected null value."))));
            separator = '&';
        }

        var uri = uriBuilder.ToString();
        System.Net.Http.HttpRequestMessage? request = null;
        try
        {
            request = new System.Net.Http.HttpRequestMessage(GET, uri);



            return request;
        }
        catch
        {
            request?.Dispose();
            throw;
        }
    }

    private System.Net.Http.HttpRequestMessage EndSession_CreateRequest(
        string? idTokenHint,
        string? postLogoutRedirectUri,
        string? state
    )
    {
        var path = "/connect/endsession";


        var uriBuilder = new System.Text.StringBuilder(path);

        var separator = '?';

        if (true && idTokenHint != null)
        {
            uriBuilder
                .Append(separator)
                .Append("id_token_hint=")
                .Append(UrlEncode(ValueToString(idTokenHint ?? throw new System.InvalidOperationException("Unexpected null value."))));
            separator = '&';
        }
        if (true && postLogoutRedirectUri != null)
        {
            uriBuilder
                .Append(separator)
                .Append("post_logout_redirect_uri=")
                .Append(UrlEncode(ValueToString(postLogoutRedirectUri ?? throw new System.InvalidOperationException("Unexpected null value."))));
            separator = '&';
        }
        if (true && state != null)
        {
            uriBuilder
                .Append(separator)
                .Append("state=")
                .Append(UrlEncode(ValueToString(state ?? throw new System.InvalidOperationException("Unexpected null value."))));
            separator = '&';
        }

        var uri = uriBuilder.ToString();
        System.Net.Http.HttpRequestMessage? request = null;
        try
        {
            request = new System.Net.Http.HttpRequestMessage(GET, uri);



            return request;
        }
        catch
        {
            request?.Dispose();
            throw;
        }
    }

    private System.Net.Http.HttpRequestMessage GetDiscoveryDocument_CreateRequest(
    )
    {
        var path = "/.well-known/openid-configuration";


        var uriBuilder = new System.Text.StringBuilder(path);


        var uri = uriBuilder.ToString();
        System.Net.Http.HttpRequestMessage? request = null;
        try
        {
            request = new System.Net.Http.HttpRequestMessage(GET, uri);



            return request;
        }
        catch
        {
            request?.Dispose();
            throw;
        }
    }

    private System.Net.Http.HttpRequestMessage GetUserInfo_CreateRequest(
    )
    {
        var path = "/connect/userinfo";


        var uriBuilder = new System.Text.StringBuilder(path);


        var uri = uriBuilder.ToString();
        System.Net.Http.HttpRequestMessage? request = null;
        try
        {
            request = new System.Net.Http.HttpRequestMessage(GET, uri);



            return request;
        }
        catch
        {
            request?.Dispose();
            throw;
        }
    }

    private System.Net.Http.HttpRequestMessage IntrospectToken_CreateRequest(
        string token
    )
    {
        var path = "/connect/introspect";


        var uriBuilder = new System.Text.StringBuilder(path);


        var uri = uriBuilder.ToString();
        System.Net.Http.HttpRequestMessage? request = null;
        try
        {
            request = new System.Net.Http.HttpRequestMessage(POST, uri);

            var contentItems = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>();
            if (true)
            {
                contentItems.Add(
                    System.Collections.Generic.KeyValuePair.Create("token",
                    ValueToString(token)));
            }
            request.Content = new System.Net.Http.FormUrlEncodedContent(contentItems);


            return request;
        }
        catch
        {
            request?.Dispose();
            throw;
        }
    }

    private System.Net.Http.HttpRequestMessage RequestAuthorizationCodeToken_CreateRequest(
        string clientId,
        GrantType grantType,
        string? clientSecret,
        string? scope,
        string? redirectUri,
        string? code,
        string? codeVerifier,
        string? refreshToken
    )
    {
        var path = "/connect/token";


        var uriBuilder = new System.Text.StringBuilder(path);


        var uri = uriBuilder.ToString();
        System.Net.Http.HttpRequestMessage? request = null;
        try
        {
            request = new System.Net.Http.HttpRequestMessage(POST, uri);

            var contentItems = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>();
            if (true)
            {
                contentItems.Add(
                    System.Collections.Generic.KeyValuePair.Create("client_id",
                    ValueToString(clientId)));
            }
            if (true)
            {
                contentItems.Add(
                    System.Collections.Generic.KeyValuePair.Create("grant_type",
                    EnumToString(grantType)));
            }
            if (true && clientSecret != null)
            {
                contentItems.Add(
                    System.Collections.Generic.KeyValuePair.Create("client_secret",
                    ValueToString(clientSecret ?? throw new System.InvalidOperationException("Unexpected null value."))));
            }
            if (true && scope != null)
            {
                contentItems.Add(
                    System.Collections.Generic.KeyValuePair.Create("scope",
                    ValueToString(scope ?? throw new System.InvalidOperationException("Unexpected null value."))));
            }
            if (true && redirectUri != null)
            {
                contentItems.Add(
                    System.Collections.Generic.KeyValuePair.Create("redirect_uri",
                    ValueToString(redirectUri ?? throw new System.InvalidOperationException("Unexpected null value."))));
            }
            if (true && code != null)
            {
                contentItems.Add(
                    System.Collections.Generic.KeyValuePair.Create("code",
                    ValueToString(code ?? throw new System.InvalidOperationException("Unexpected null value."))));
            }
            if (true && codeVerifier != null)
            {
                contentItems.Add(
                    System.Collections.Generic.KeyValuePair.Create("code_verifier",
                    ValueToString(codeVerifier ?? throw new System.InvalidOperationException("Unexpected null value."))));
            }
            if (true && refreshToken != null)
            {
                contentItems.Add(
                    System.Collections.Generic.KeyValuePair.Create("refresh_token",
                    ValueToString(refreshToken ?? throw new System.InvalidOperationException("Unexpected null value."))));
            }
            request.Content = new System.Net.Http.FormUrlEncodedContent(contentItems);


            return request;
        }
        catch
        {
            request?.Dispose();
            throw;
        }
    }

    private System.Net.Http.HttpRequestMessage RevokeToken_CreateRequest(
        string token,
        TokenType? tokenTypeHint
    )
    {
        var path = "/connect/revocation";


        var uriBuilder = new System.Text.StringBuilder(path);


        var uri = uriBuilder.ToString();
        System.Net.Http.HttpRequestMessage? request = null;
        try
        {
            request = new System.Net.Http.HttpRequestMessage(POST, uri);

            var contentItems = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>();
            if (true)
            {
                contentItems.Add(
                    System.Collections.Generic.KeyValuePair.Create("token",
                    ValueToString(token)));
            }
            if (true && tokenTypeHint != null)
            {
                contentItems.Add(
                    System.Collections.Generic.KeyValuePair.Create("token_type_hint",
                    EnumToString(tokenTypeHint ?? throw new System.InvalidOperationException("Unexpected null value."))));
            }
            request.Content = new System.Net.Http.FormUrlEncodedContent(contentItems);


            return request;
        }
        catch
        {
            request?.Dispose();
            throw;
        }
    }

    ~ConnectClient()
    {
        Dispose();
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static")]
    partial void ValueToString<T>(T value, ref string? result) where T : notnull;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static")]
    partial void EnumToString<T>(T value, ref string? result) where T : struct, System.Enum;

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _httpClient.Dispose();
        }
    }

    private string ValueToString<T>(T value) where T : notnull
    {
        string? result = null;
        ValueToString(value, ref result);

        if (result != null)
        {
            return result;
        }

        return value.ToString()
            ?? throw new System.InvalidOperationException("UrlEncode returned null.");
    }

    private string EnumToString<T>(T value) where T : struct, System.Enum
    {
        string? result = null;
        EnumToString(value, ref result);
        return result ?? ValueToString(value);
    }

    private static string UrlEncode(string value)
    {
        return System.Web.HttpUtility.UrlEncode(value.ToString())
            ?? throw new System.InvalidOperationException("UrlEncode returned null.");
    }
}

public partial interface IConnectClient
{
    /// <summary>
    /// Initializes an OAuth 2.0 flow using a response_type. See RFC 6749 (https://tools.ietf.org/html/rfc6749) for more details. 
    /// </summary>
    System.Threading.Tasks.Task Authorize(
        string clientId,
        ResponseType responseType,
        string redirectUri,
        string scope,
        string? codeChallenge,
        CodeChallengeMethod? codeChallengeMethod,
        ResponseMode? responseMode,
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// The end session endpoint can be used to trigger single sign-out in the browser.
    /// </summary>
    System.Threading.Tasks.Task EndSession(
        string? idTokenHint,
        string? postLogoutRedirectUri,
        string? state,
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// This endpoint follows the specification defined at http://openid.net/specs/openid-connect-discovery-1_0.html#ProviderMetadata. It provides a mechanism for an OpenID Connect Relying Party to discover the End-User&#39;s OpenID Provider and obtain information needed to interact with it, including its OAuth 2.0 endpoint locations. 
    /// </summary>
    System.Threading.Tasks.Task<DiscoveryDocumentResponse> GetDiscoveryDocument(
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// This API reflects an implementation according to OpenID Connect. This endpoint returns a message with user details. The content depends on the SCOPE associated with the given access_token. IMPORTANT: The API will fail if no active id_token is available for the associated user. For more information refer to this website: http://openid.net/specs/openid-connect-core-1_0.html#UserInfo. 
    /// </summary>
    System.Threading.Tasks.Task<UserInfo> GetUserInfo(
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// The introspection endpoint is an implementation of RFC 7662.
    /// </summary>
    System.Threading.Tasks.Task<IntrospectionResponse> IntrospectToken(
        string token,
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Request access_token or refresh_token using OAuth 2.0 grant_type. See RFC 6749 for more details. 
    /// </summary>
    System.Threading.Tasks.Task<TokenData> RequestAuthorizationCodeToken(
        string clientId,
        GrantType grantType,
        string? clientSecret,
        string? scope,
        string? redirectUri,
        string? code,
        string? codeVerifier,
        string? refreshToken,
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// This endpoint allows revoking access tokens (reference tokens only) and refresh token. It implements the token revocation specification (RFC 7009). 
    /// </summary>
    System.Threading.Tasks.Task RevokeToken(
        string token,
        TokenType? tokenTypeHint,
        System.Threading.CancellationToken cancellationToken = default
    );

}

#nullable restore
