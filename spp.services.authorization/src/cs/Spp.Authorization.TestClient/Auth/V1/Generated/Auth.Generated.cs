#nullable enable

namespace Spp.Authorization.TestClient.Auth.V1;

public partial class AuthTestClient : IAuthTestClient
{
    private const int Default = -1;
    private static readonly System.Net.Http.HttpMethod DELETE = System.Net.Http.HttpMethod.Delete;
    private static readonly System.Net.Http.HttpMethod GET = System.Net.Http.HttpMethod.Get;
    private static readonly System.Net.Http.HttpMethod POST = System.Net.Http.HttpMethod.Post;
    private static readonly System.Net.Http.HttpMethod PUT = System.Net.Http.HttpMethod.Put;

    private readonly System.Net.Http.HttpClient _httpClient;
    private readonly System.Text.Json.JsonSerializerOptions _jsonSerializerOptions;

    public AuthTestClient(System.Net.Http.HttpClient httpClient, System.Text.Json.JsonSerializerOptions jsonSerializerOptions)
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
    /// Get user info.
    /// </summary>
    public async System.Threading.Tasks.Task<IAuthTestClient.GetUserInfoClientResponse> GetUserInfo(
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
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<GetUserInfoResponse?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 200, but content is null.");

            return IAuthTestClient.GetUserInfoClientResponse.Create200(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 401 || 401 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 401, but content is null.");

            return IAuthTestClient.GetUserInfoClientResponse.Create401(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 403 || 403 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 403, but content is null.");

            return IAuthTestClient.GetUserInfoClientResponse.Create403(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 404 || 404 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 404, but content is null.");

            return IAuthTestClient.GetUserInfoClientResponse.Create404(response.StatusCode, response.Headers, content);
        }

        var bytes = await response.Content.ReadAsByteArrayAsync(cancellationToken);
        return IAuthTestClient.GetUserInfoClientResponse.CreateDefault(response.StatusCode, response.Headers, bytes);
    }

    /// <summary>
    /// Get user info.
    /// </summary>
    public async System.Threading.Tasks.Task<IAuthTestClient.GetUserInfoClientResponse> GetUserInfo(
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = GetUserInfo_CreateRequest(
        );

        return await GetUserInfo(request, cancellationToken);
    }
    /// <summary>
    /// Sign-in with IdentityProvider.
    /// </summary>
    public async System.Threading.Tasks.Task<IAuthTestClient.SignInWithIndentityProviderClientResponse> SignInWithIndentityProvider(
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

        if ((int)response.StatusCode == 302 || 302 == -1)
        {

            return IAuthTestClient.SignInWithIndentityProviderClientResponse.Create302(response.StatusCode, response.Headers);
        }
        if ((int)response.StatusCode == 400 || 400 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 400, but content is null.");

            return IAuthTestClient.SignInWithIndentityProviderClientResponse.Create400(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == Default || Default == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is Default, but content is null.");

            return IAuthTestClient.SignInWithIndentityProviderClientResponse.CreateDefault(response.StatusCode, response.Headers, content);
        }

#pragma warning disable CS0162 // Code is unreachable
        throw new System.InvalidOperationException("Code is unreachable.");
#pragma warning restore CS0162 // Code is unreachable
    }

    /// <summary>
    /// Sign-in with IdentityProvider.
    /// </summary>
    public async System.Threading.Tasks.Task<IAuthTestClient.SignInWithIndentityProviderClientResponse> SignInWithIndentityProvider(
        string redirectUri,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = SignInWithIndentityProvider_CreateRequest(
            redirectUri
        );

        return await SignInWithIndentityProvider(request, cancellationToken);
    }

    private System.Net.Http.HttpRequestMessage GetUserInfo_CreateRequest(
    )
    {
        var path = "/v1/auth/user-info";


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

    private System.Net.Http.HttpRequestMessage SignInWithIndentityProvider_CreateRequest(
        string redirectUri
    )
    {
        var path = "/v1/auth/sign-in/identity-provider";


        var uriBuilder = new System.Text.StringBuilder(path);

        var separator = '?';

        if (true)
        {
            uriBuilder
                .Append(separator)
                .Append("redirect_uri=")
                .Append(UrlEncode(ValueToString(redirectUri)));
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

    ~AuthTestClient()
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

public partial interface IAuthTestClient : System.IDisposable
{
    private const int Default = -1;

    /// <summary>
    /// Get user info.
    /// </summary>
    System.Threading.Tasks.Task<GetUserInfoClientResponse> GetUserInfo(
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Sign-in with IdentityProvider.
    /// </summary>
    System.Threading.Tasks.Task<SignInWithIndentityProviderClientResponse> SignInWithIndentityProvider(
        string redirectUri,
        System.Threading.CancellationToken cancellationToken = default
    );

    public class GetUserInfoClientResponse
    {
        private readonly GetUserInfoResponse? _content200;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content401;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content403;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content404;
        private readonly byte[]? _contentDefault;

        private GetUserInfoClientResponse(
            System.Net.HttpStatusCode statusCode,
            System.Net.Http.Headers.HttpResponseHeaders headers,
            GetUserInfoResponse? content200 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content401 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content403 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content404 = null,
            byte[]? contentDefault = null
        )
        {
            StatusCode = statusCode;
            Headers = headers;
            _content200 = content200;
            _content401 = content401;
            _content403 = content403;
            _content404 = content404;
            _contentDefault = contentDefault;
        }

        public System.Net.HttpStatusCode StatusCode { get; }

        public System.Net.Http.Headers.HttpResponseHeaders Headers { get; }

        public GetUserInfoResponse Content200
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

        public Microsoft.AspNetCore.Mvc.ProblemDetails Content403
        {
            get
            {
                if ((int)StatusCode != 403 && 403 != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not 403.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _content403 ?? throw new System.InvalidOperationException("Status code is 403, but result is null.");
            }
        }

        public Microsoft.AspNetCore.Mvc.ProblemDetails Content404
        {
            get
            {
                if ((int)StatusCode != 404 && 404 != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not 404.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _content404 ?? throw new System.InvalidOperationException("Status code is 404, but result is null.");
            }
        }

        public byte[] ContentDefault => _contentDefault ?? throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not Default.");

        public void EnsureStatusCode(System.Net.HttpStatusCode code)
        {
            if (StatusCode != code)
            {
                throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + $", not {code}.");
            }
        }

        public static GetUserInfoClientResponse Create200(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, GetUserInfoResponse content)
        {
            if ((int)code != 200 && 200 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 200 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new GetUserInfoClientResponse(statusCode: code, headers: headers, content200: content);
        }

        public static GetUserInfoClientResponse Create401(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 401 && 401 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 401 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new GetUserInfoClientResponse(statusCode: code, headers: headers, content401: content);
        }

        public static GetUserInfoClientResponse Create403(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 403 && 403 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 403 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new GetUserInfoClientResponse(statusCode: code, headers: headers, content403: content);
        }

        public static GetUserInfoClientResponse Create404(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 404 && 404 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 404 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new GetUserInfoClientResponse(statusCode: code, headers: headers, content404: content);
        }

        public static GetUserInfoClientResponse CreateDefault(
            System.Net.HttpStatusCode code,
            System.Net.Http.Headers.HttpResponseHeaders headers,
            byte[] content)
        {
            if ((int)code == 200)
            {
                throw new System.InvalidOperationException("Status code " + code.ToString() + " is specified.");
            }
            if ((int)code == 401)
            {
                throw new System.InvalidOperationException("Status code " + code.ToString() + " is specified.");
            }
            if ((int)code == 403)
            {
                throw new System.InvalidOperationException("Status code " + code.ToString() + " is specified.");
            }
            if ((int)code == 404)
            {
                throw new System.InvalidOperationException("Status code " + code.ToString() + " is specified.");
            }

            return new GetUserInfoClientResponse(statusCode: code, headers: headers, contentDefault: content);
        }
    }
    public class SignInWithIndentityProviderClientResponse
    {
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content400;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _contentDefault;

        private SignInWithIndentityProviderClientResponse(
            System.Net.HttpStatusCode statusCode,
            System.Net.Http.Headers.HttpResponseHeaders headers,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content400 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? contentDefault = null
        )
        {
            StatusCode = statusCode;
            Headers = headers;
            _content400 = content400;
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

        public static SignInWithIndentityProviderClientResponse Create302(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers)
        {
            if ((int)code != 302 && 302 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 302 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new SignInWithIndentityProviderClientResponse(statusCode: code, headers: headers);
        }

        public static SignInWithIndentityProviderClientResponse Create400(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 400 && 400 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 400 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new SignInWithIndentityProviderClientResponse(statusCode: code, headers: headers, content400: content);
        }

        public static SignInWithIndentityProviderClientResponse CreateDefault(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code Default is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new SignInWithIndentityProviderClientResponse(statusCode: code, headers: headers, contentDefault: content);
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
}

#nullable restore
