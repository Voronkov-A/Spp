#nullable enable

namespace Spp.Authorization.TestClient.Connect.V1;

public partial class ConnectTestClient : IConnectTestClient
{
    private const int Default = -1;
    private static readonly System.Net.Http.HttpMethod DELETE = System.Net.Http.HttpMethod.Delete;
    private static readonly System.Net.Http.HttpMethod GET = System.Net.Http.HttpMethod.Get;
    private static readonly System.Net.Http.HttpMethod POST = System.Net.Http.HttpMethod.Post;
    private static readonly System.Net.Http.HttpMethod PUT = System.Net.Http.HttpMethod.Put;

    private readonly System.Net.Http.HttpClient _httpClient;
    private readonly System.Text.Json.JsonSerializerOptions _jsonSerializerOptions;

    public ConnectTestClient(System.Net.Http.HttpClient httpClient, System.Text.Json.JsonSerializerOptions jsonSerializerOptions)
    {
        _httpClient = httpClient;
        _jsonSerializerOptions = jsonSerializerOptions;
    }

    public void Dispose()
    {
        Dispose(true);
        System.GC.SuppressFinalize(this);
    }

    /// <summary>
    /// The end session endpoint can be used to trigger single sign-out in the browser.
    /// </summary>
    public async System.Threading.Tasks.Task<IConnectTestClient.EndSessionClientResponse> EndSession(
        System.Net.Http.HttpRequestMessage request,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        if (_httpClient.BaseAddress != null
            && request.RequestUri != null
            && !_httpClient.BaseAddress.IsBaseOf(request.RequestUri))
        {
            throw new System.InvalidOperationException(
                $"{_httpClient.BaseAddress} is not a base address for {request.RequestUri}.");
        }

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode == 204 || 204 == -1)
        {

            return IConnectTestClient.EndSessionClientResponse.Create204(response.StatusCode, response.Headers);
        }
        if ((int)response.StatusCode == 302 || 302 == -1)
        {

            return IConnectTestClient.EndSessionClientResponse.Create302(response.StatusCode, response.Headers);
        }
        if ((int)response.StatusCode == 400 || 400 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 400, but content is null.");

            return IConnectTestClient.EndSessionClientResponse.Create400(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 401 || 401 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 401, but content is null.");

            return IConnectTestClient.EndSessionClientResponse.Create401(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == Default || Default == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is Default, but content is null.");

            return IConnectTestClient.EndSessionClientResponse.CreateDefault(response.StatusCode, response.Headers, content);
        }

#pragma warning disable CS0162 // Code is unreachable
        throw new System.InvalidOperationException("Code is unreachable.");
#pragma warning restore CS0162 // Code is unreachable
    }

    /// <summary>
    /// The end session endpoint can be used to trigger single sign-out in the browser.
    /// </summary>
    public async System.Threading.Tasks.Task<IConnectTestClient.EndSessionClientResponse> EndSession(
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

        return await EndSession(request, cancellationToken);
    }
    /// <summary>
    /// This endpoint follows the specification defined at http://openid.net/specs/openid-connect-discovery-1_0.html#ProviderMetadata. It provides a mechanism for an OpenID Connect Relying Party to discover the End-User&#39;s OpenID Provider and obtain information needed to interact with it, including its OAuth 2.0 endpoint locations. 
    /// </summary>
    public async System.Threading.Tasks.Task<IConnectTestClient.GetDiscoveryDocumentClientResponse> GetDiscoveryDocument(
        System.Net.Http.HttpRequestMessage request,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        if (_httpClient.BaseAddress != null
            && request.RequestUri != null
            && !_httpClient.BaseAddress.IsBaseOf(request.RequestUri))
        {
            throw new System.InvalidOperationException(
                $"{_httpClient.BaseAddress} is not a base address for {request.RequestUri}.");
        }

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode == 200 || 200 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<DiscoveryDocumentResponse?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 200, but content is null.");

            return IConnectTestClient.GetDiscoveryDocumentClientResponse.Create200(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == Default || Default == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is Default, but content is null.");

            return IConnectTestClient.GetDiscoveryDocumentClientResponse.CreateDefault(response.StatusCode, response.Headers, content);
        }

#pragma warning disable CS0162 // Code is unreachable
        throw new System.InvalidOperationException("Code is unreachable.");
#pragma warning restore CS0162 // Code is unreachable
    }

    /// <summary>
    /// This endpoint follows the specification defined at http://openid.net/specs/openid-connect-discovery-1_0.html#ProviderMetadata. It provides a mechanism for an OpenID Connect Relying Party to discover the End-User&#39;s OpenID Provider and obtain information needed to interact with it, including its OAuth 2.0 endpoint locations. 
    /// </summary>
    public async System.Threading.Tasks.Task<IConnectTestClient.GetDiscoveryDocumentClientResponse> GetDiscoveryDocument(
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = GetDiscoveryDocument_CreateRequest(
        );

        return await GetDiscoveryDocument(request, cancellationToken);
    }
    /// <summary>
    /// The introspection endpoint is an implementation of RFC 7662.
    /// </summary>
    public async System.Threading.Tasks.Task<IConnectTestClient.IntrospectTokenClientResponse> IntrospectToken(
        System.Net.Http.HttpRequestMessage request,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        if (_httpClient.BaseAddress != null
            && request.RequestUri != null
            && !_httpClient.BaseAddress.IsBaseOf(request.RequestUri))
        {
            throw new System.InvalidOperationException(
                $"{_httpClient.BaseAddress} is not a base address for {request.RequestUri}.");
        }

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode == 200 || 200 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<IntrospectionResponse?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 200, but content is null.");

            return IConnectTestClient.IntrospectTokenClientResponse.Create200(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 400 || 400 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 400, but content is null.");

            return IConnectTestClient.IntrospectTokenClientResponse.Create400(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 401 || 401 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 401, but content is null.");

            return IConnectTestClient.IntrospectTokenClientResponse.Create401(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == Default || Default == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is Default, but content is null.");

            return IConnectTestClient.IntrospectTokenClientResponse.CreateDefault(response.StatusCode, response.Headers, content);
        }

#pragma warning disable CS0162 // Code is unreachable
        throw new System.InvalidOperationException("Code is unreachable.");
#pragma warning restore CS0162 // Code is unreachable
    }

    /// <summary>
    /// The introspection endpoint is an implementation of RFC 7662.
    /// </summary>
    public async System.Threading.Tasks.Task<IConnectTestClient.IntrospectTokenClientResponse> IntrospectToken(
        string token,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = IntrospectToken_CreateRequest(
            token
        );

        return await IntrospectToken(request, cancellationToken);
    }
    /// <summary>
    /// Request access_token or refresh_token using OAuth 2.0 grant_type. See RFC 6749 for more details. 
    /// </summary>
    public async System.Threading.Tasks.Task<IConnectTestClient.RequestAuthorizationCodeTokenClientResponse> RequestAuthorizationCodeToken(
        System.Net.Http.HttpRequestMessage request,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        if (_httpClient.BaseAddress != null
            && request.RequestUri != null
            && !_httpClient.BaseAddress.IsBaseOf(request.RequestUri))
        {
            throw new System.InvalidOperationException(
                $"{_httpClient.BaseAddress} is not a base address for {request.RequestUri}.");
        }

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode == 200 || 200 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<TokenData?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 200, but content is null.");

            return IConnectTestClient.RequestAuthorizationCodeTokenClientResponse.Create200(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 400 || 400 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 400, but content is null.");

            return IConnectTestClient.RequestAuthorizationCodeTokenClientResponse.Create400(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 401 || 401 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 401, but content is null.");

            return IConnectTestClient.RequestAuthorizationCodeTokenClientResponse.Create401(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == Default || Default == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is Default, but content is null.");

            return IConnectTestClient.RequestAuthorizationCodeTokenClientResponse.CreateDefault(response.StatusCode, response.Headers, content);
        }

#pragma warning disable CS0162 // Code is unreachable
        throw new System.InvalidOperationException("Code is unreachable.");
#pragma warning restore CS0162 // Code is unreachable
    }

    /// <summary>
    /// Request access_token or refresh_token using OAuth 2.0 grant_type. See RFC 6749 for more details. 
    /// </summary>
    public async System.Threading.Tasks.Task<IConnectTestClient.RequestAuthorizationCodeTokenClientResponse> RequestAuthorizationCodeToken(
        string clientId,
        GrantType grantType,
        string clientSecret,
        string? scope,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = RequestAuthorizationCodeToken_CreateRequest(
            clientId,
            grantType,
            clientSecret,
            scope
        );

        return await RequestAuthorizationCodeToken(request, cancellationToken);
    }
    /// <summary>
    /// This endpoint allows revoking access tokens (reference tokens only) and refresh token. It implements the token revocation specification (RFC 7009). 
    /// </summary>
    public async System.Threading.Tasks.Task<IConnectTestClient.RevokeTokenClientResponse> RevokeToken(
        System.Net.Http.HttpRequestMessage request,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        if (_httpClient.BaseAddress != null
            && request.RequestUri != null
            && !_httpClient.BaseAddress.IsBaseOf(request.RequestUri))
        {
            throw new System.InvalidOperationException(
                $"{_httpClient.BaseAddress} is not a base address for {request.RequestUri}.");
        }

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode == 204 || 204 == -1)
        {

            return IConnectTestClient.RevokeTokenClientResponse.Create204(response.StatusCode, response.Headers);
        }
        if ((int)response.StatusCode == 400 || 400 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 400, but content is null.");

            return IConnectTestClient.RevokeTokenClientResponse.Create400(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 401 || 401 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 401, but content is null.");

            return IConnectTestClient.RevokeTokenClientResponse.Create401(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == Default || Default == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is Default, but content is null.");

            return IConnectTestClient.RevokeTokenClientResponse.CreateDefault(response.StatusCode, response.Headers, content);
        }

#pragma warning disable CS0162 // Code is unreachable
        throw new System.InvalidOperationException("Code is unreachable.");
#pragma warning restore CS0162 // Code is unreachable
    }

    /// <summary>
    /// This endpoint allows revoking access tokens (reference tokens only) and refresh token. It implements the token revocation specification (RFC 7009). 
    /// </summary>
    public async System.Threading.Tasks.Task<IConnectTestClient.RevokeTokenClientResponse> RevokeToken(
        string token,
        TokenType? tokenTypeHint,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = RevokeToken_CreateRequest(
            token,
            tokenTypeHint
        );

        return await RevokeToken(request, cancellationToken);
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
        string clientSecret,
        string? scope
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
            if (true)
            {
                contentItems.Add(
                    System.Collections.Generic.KeyValuePair.Create("client_secret",
                    ValueToString(clientSecret)));
            }
            if (true && scope != null)
            {
                contentItems.Add(
                    System.Collections.Generic.KeyValuePair.Create("scope",
                    ValueToString(scope ?? throw new System.InvalidOperationException("Unexpected null value."))));
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

    ~ConnectTestClient()
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

public partial interface IConnectTestClient : System.IDisposable
{
    private const int Default = -1;

    /// <summary>
    /// The end session endpoint can be used to trigger single sign-out in the browser.
    /// </summary>
    System.Threading.Tasks.Task<EndSessionClientResponse> EndSession(
        string? idTokenHint,
        string? postLogoutRedirectUri,
        string? state,
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// This endpoint follows the specification defined at http://openid.net/specs/openid-connect-discovery-1_0.html#ProviderMetadata. It provides a mechanism for an OpenID Connect Relying Party to discover the End-User&#39;s OpenID Provider and obtain information needed to interact with it, including its OAuth 2.0 endpoint locations. 
    /// </summary>
    System.Threading.Tasks.Task<GetDiscoveryDocumentClientResponse> GetDiscoveryDocument(
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// The introspection endpoint is an implementation of RFC 7662.
    /// </summary>
    System.Threading.Tasks.Task<IntrospectTokenClientResponse> IntrospectToken(
        string token,
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Request access_token or refresh_token using OAuth 2.0 grant_type. See RFC 6749 for more details. 
    /// </summary>
    System.Threading.Tasks.Task<RequestAuthorizationCodeTokenClientResponse> RequestAuthorizationCodeToken(
        string clientId,
        GrantType grantType,
        string clientSecret,
        string? scope,
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// This endpoint allows revoking access tokens (reference tokens only) and refresh token. It implements the token revocation specification (RFC 7009). 
    /// </summary>
    System.Threading.Tasks.Task<RevokeTokenClientResponse> RevokeToken(
        string token,
        TokenType? tokenTypeHint,
        System.Threading.CancellationToken cancellationToken = default
    );

    public class EndSessionClientResponse
    {
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content400;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content401;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _contentDefault;

        private EndSessionClientResponse(
            System.Net.HttpStatusCode statusCode,
            System.Net.Http.Headers.HttpResponseHeaders headers,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content400 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content401 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? contentDefault = null
        )
        {
            StatusCode = statusCode;
            Headers = headers;
            _content400 = content400;
            _content401 = content401;
            _contentDefault = contentDefault;
        }

        public System.Net.HttpStatusCode StatusCode { get; }

        public System.Net.Http.Headers.HttpResponseHeaders Headers { get; }



        public HeaderCollection302 Headers302 => new HeaderCollection302(Headers);

        public Microsoft.AspNetCore.Mvc.ProblemDetails Content400
        {
            get
            {
                if ((int)StatusCode != 400 && 400 != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not 400.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _content400 ?? throw new System.InvalidOperationException("Status code is 400, but result is null.");
            }
        }

        public Microsoft.AspNetCore.Mvc.ProblemDetails Content401
        {
            get
            {
                if ((int)StatusCode != 401 && 401 != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not 401.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _content401 ?? throw new System.InvalidOperationException("Status code is 401, but result is null.");
            }
        }

        public Microsoft.AspNetCore.Mvc.ProblemDetails ContentDefault
        {
            get
            {
                if ((int)StatusCode != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not Default.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _contentDefault ?? throw new System.InvalidOperationException("Status code is Default, but result is null.");
            }
        }


        public void EnsureStatusCode(System.Net.HttpStatusCode code)
        {
            if (StatusCode != code)
            {
                throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + $", not {code}.");
            }
        }

        public static EndSessionClientResponse Create204(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers)
        {
            if ((int)code != 204 && 204 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 204 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new EndSessionClientResponse(statusCode: code, headers: headers);
        }

        public static EndSessionClientResponse Create302(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers)
        {
            if ((int)code != 302 && 302 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 302 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new EndSessionClientResponse(statusCode: code, headers: headers);
        }

        public static EndSessionClientResponse Create400(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 400 && 400 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 400 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new EndSessionClientResponse(statusCode: code, headers: headers, content400: content);
        }

        public static EndSessionClientResponse Create401(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 401 && 401 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 401 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new EndSessionClientResponse(statusCode: code, headers: headers, content401: content);
        }

        public static EndSessionClientResponse CreateDefault(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code Default is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new EndSessionClientResponse(statusCode: code, headers: headers, contentDefault: content);
        }

        public readonly struct HeaderCollection302
        {
            private readonly System.Net.Http.Headers.HttpResponseHeaders _headers;

            public HeaderCollection302(System.Net.Http.Headers.HttpResponseHeaders headers)
            {
                _headers = headers;
            }

            public string? Location =>
                System.Linq.Enumerable.SingleOrDefault(_headers.GetValues("Location"));
        }
    }
    public class GetDiscoveryDocumentClientResponse
    {
        private readonly DiscoveryDocumentResponse? _content200;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _contentDefault;

        private GetDiscoveryDocumentClientResponse(
            System.Net.HttpStatusCode statusCode,
            System.Net.Http.Headers.HttpResponseHeaders headers,
            DiscoveryDocumentResponse? content200 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? contentDefault = null
        )
        {
            StatusCode = statusCode;
            Headers = headers;
            _content200 = content200;
            _contentDefault = contentDefault;
        }

        public System.Net.HttpStatusCode StatusCode { get; }

        public System.Net.Http.Headers.HttpResponseHeaders Headers { get; }

        public DiscoveryDocumentResponse Content200
        {
            get
            {
                if ((int)StatusCode != 200 && 200 != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not 200.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _content200 ?? throw new System.InvalidOperationException("Status code is 200, but result is null.");
            }
        }

        public Microsoft.AspNetCore.Mvc.ProblemDetails ContentDefault
        {
            get
            {
                if ((int)StatusCode != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not Default.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _contentDefault ?? throw new System.InvalidOperationException("Status code is Default, but result is null.");
            }
        }


        public void EnsureStatusCode(System.Net.HttpStatusCode code)
        {
            if (StatusCode != code)
            {
                throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + $", not {code}.");
            }
        }

        public static GetDiscoveryDocumentClientResponse Create200(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, DiscoveryDocumentResponse content)
        {
            if ((int)code != 200 && 200 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 200 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new GetDiscoveryDocumentClientResponse(statusCode: code, headers: headers, content200: content);
        }

        public static GetDiscoveryDocumentClientResponse CreateDefault(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code Default is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new GetDiscoveryDocumentClientResponse(statusCode: code, headers: headers, contentDefault: content);
        }

    }
    public class IntrospectTokenClientResponse
    {
        private readonly IntrospectionResponse? _content200;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content400;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content401;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _contentDefault;

        private IntrospectTokenClientResponse(
            System.Net.HttpStatusCode statusCode,
            System.Net.Http.Headers.HttpResponseHeaders headers,
            IntrospectionResponse? content200 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content400 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content401 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? contentDefault = null
        )
        {
            StatusCode = statusCode;
            Headers = headers;
            _content200 = content200;
            _content400 = content400;
            _content401 = content401;
            _contentDefault = contentDefault;
        }

        public System.Net.HttpStatusCode StatusCode { get; }

        public System.Net.Http.Headers.HttpResponseHeaders Headers { get; }

        public IntrospectionResponse Content200
        {
            get
            {
                if ((int)StatusCode != 200 && 200 != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not 200.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _content200 ?? throw new System.InvalidOperationException("Status code is 200, but result is null.");
            }
        }

        public Microsoft.AspNetCore.Mvc.ProblemDetails Content400
        {
            get
            {
                if ((int)StatusCode != 400 && 400 != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not 400.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _content400 ?? throw new System.InvalidOperationException("Status code is 400, but result is null.");
            }
        }

        public Microsoft.AspNetCore.Mvc.ProblemDetails Content401
        {
            get
            {
                if ((int)StatusCode != 401 && 401 != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not 401.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _content401 ?? throw new System.InvalidOperationException("Status code is 401, but result is null.");
            }
        }

        public Microsoft.AspNetCore.Mvc.ProblemDetails ContentDefault
        {
            get
            {
                if ((int)StatusCode != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not Default.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _contentDefault ?? throw new System.InvalidOperationException("Status code is Default, but result is null.");
            }
        }


        public void EnsureStatusCode(System.Net.HttpStatusCode code)
        {
            if (StatusCode != code)
            {
                throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + $", not {code}.");
            }
        }

        public static IntrospectTokenClientResponse Create200(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, IntrospectionResponse content)
        {
            if ((int)code != 200 && 200 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 200 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new IntrospectTokenClientResponse(statusCode: code, headers: headers, content200: content);
        }

        public static IntrospectTokenClientResponse Create400(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 400 && 400 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 400 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new IntrospectTokenClientResponse(statusCode: code, headers: headers, content400: content);
        }

        public static IntrospectTokenClientResponse Create401(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 401 && 401 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 401 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new IntrospectTokenClientResponse(statusCode: code, headers: headers, content401: content);
        }

        public static IntrospectTokenClientResponse CreateDefault(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code Default is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new IntrospectTokenClientResponse(statusCode: code, headers: headers, contentDefault: content);
        }

    }
    public class RequestAuthorizationCodeTokenClientResponse
    {
        private readonly TokenData? _content200;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content400;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content401;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _contentDefault;

        private RequestAuthorizationCodeTokenClientResponse(
            System.Net.HttpStatusCode statusCode,
            System.Net.Http.Headers.HttpResponseHeaders headers,
            TokenData? content200 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content400 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content401 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? contentDefault = null
        )
        {
            StatusCode = statusCode;
            Headers = headers;
            _content200 = content200;
            _content400 = content400;
            _content401 = content401;
            _contentDefault = contentDefault;
        }

        public System.Net.HttpStatusCode StatusCode { get; }

        public System.Net.Http.Headers.HttpResponseHeaders Headers { get; }

        public TokenData Content200
        {
            get
            {
                if ((int)StatusCode != 200 && 200 != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not 200.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _content200 ?? throw new System.InvalidOperationException("Status code is 200, but result is null.");
            }
        }

        public Microsoft.AspNetCore.Mvc.ProblemDetails Content400
        {
            get
            {
                if ((int)StatusCode != 400 && 400 != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not 400.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _content400 ?? throw new System.InvalidOperationException("Status code is 400, but result is null.");
            }
        }

        public Microsoft.AspNetCore.Mvc.ProblemDetails Content401
        {
            get
            {
                if ((int)StatusCode != 401 && 401 != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not 401.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _content401 ?? throw new System.InvalidOperationException("Status code is 401, but result is null.");
            }
        }

        public Microsoft.AspNetCore.Mvc.ProblemDetails ContentDefault
        {
            get
            {
                if ((int)StatusCode != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not Default.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _contentDefault ?? throw new System.InvalidOperationException("Status code is Default, but result is null.");
            }
        }


        public void EnsureStatusCode(System.Net.HttpStatusCode code)
        {
            if (StatusCode != code)
            {
                throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + $", not {code}.");
            }
        }

        public static RequestAuthorizationCodeTokenClientResponse Create200(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, TokenData content)
        {
            if ((int)code != 200 && 200 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 200 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new RequestAuthorizationCodeTokenClientResponse(statusCode: code, headers: headers, content200: content);
        }

        public static RequestAuthorizationCodeTokenClientResponse Create400(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 400 && 400 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 400 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new RequestAuthorizationCodeTokenClientResponse(statusCode: code, headers: headers, content400: content);
        }

        public static RequestAuthorizationCodeTokenClientResponse Create401(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 401 && 401 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 401 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new RequestAuthorizationCodeTokenClientResponse(statusCode: code, headers: headers, content401: content);
        }

        public static RequestAuthorizationCodeTokenClientResponse CreateDefault(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code Default is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new RequestAuthorizationCodeTokenClientResponse(statusCode: code, headers: headers, contentDefault: content);
        }

    }
    public class RevokeTokenClientResponse
    {
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content400;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content401;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _contentDefault;

        private RevokeTokenClientResponse(
            System.Net.HttpStatusCode statusCode,
            System.Net.Http.Headers.HttpResponseHeaders headers,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content400 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content401 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? contentDefault = null
        )
        {
            StatusCode = statusCode;
            Headers = headers;
            _content400 = content400;
            _content401 = content401;
            _contentDefault = contentDefault;
        }

        public System.Net.HttpStatusCode StatusCode { get; }

        public System.Net.Http.Headers.HttpResponseHeaders Headers { get; }


        public Microsoft.AspNetCore.Mvc.ProblemDetails Content400
        {
            get
            {
                if ((int)StatusCode != 400 && 400 != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not 400.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _content400 ?? throw new System.InvalidOperationException("Status code is 400, but result is null.");
            }
        }

        public Microsoft.AspNetCore.Mvc.ProblemDetails Content401
        {
            get
            {
                if ((int)StatusCode != 401 && 401 != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not 401.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _content401 ?? throw new System.InvalidOperationException("Status code is 401, but result is null.");
            }
        }

        public Microsoft.AspNetCore.Mvc.ProblemDetails ContentDefault
        {
            get
            {
                if ((int)StatusCode != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not Default.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _contentDefault ?? throw new System.InvalidOperationException("Status code is Default, but result is null.");
            }
        }


        public void EnsureStatusCode(System.Net.HttpStatusCode code)
        {
            if (StatusCode != code)
            {
                throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + $", not {code}.");
            }
        }

        public static RevokeTokenClientResponse Create204(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers)
        {
            if ((int)code != 204 && 204 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 204 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new RevokeTokenClientResponse(statusCode: code, headers: headers);
        }

        public static RevokeTokenClientResponse Create400(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 400 && 400 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 400 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new RevokeTokenClientResponse(statusCode: code, headers: headers, content400: content);
        }

        public static RevokeTokenClientResponse Create401(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 401 && 401 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 401 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new RevokeTokenClientResponse(statusCode: code, headers: headers, content401: content);
        }

        public static RevokeTokenClientResponse CreateDefault(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code Default is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new RevokeTokenClientResponse(statusCode: code, headers: headers, contentDefault: content);
        }

    }
}

#nullable restore
