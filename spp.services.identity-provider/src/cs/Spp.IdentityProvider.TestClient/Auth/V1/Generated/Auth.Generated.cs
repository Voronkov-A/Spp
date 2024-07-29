#nullable enable

namespace Spp.IdentityProvider.TestClient.Auth.V1;

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
    /// Authorization callback for debug purposes.
    /// </summary>
    public async System.Threading.Tasks.Task<IAuthTestClient.CallbackClientResponse> Callback(
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
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<AuthorizationCallbackParameters?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 200, but content is null.");

            return IAuthTestClient.CallbackClientResponse.Create200(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 400 || 400 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 400, but content is null.");

            return IAuthTestClient.CallbackClientResponse.Create400(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == Default || Default == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is Default, but content is null.");

            return IAuthTestClient.CallbackClientResponse.CreateDefault(response.StatusCode, response.Headers, content);
        }

#pragma warning disable CS0162 // Code is unreachable
        throw new System.InvalidOperationException("Code is unreachable.");
#pragma warning restore CS0162 // Code is unreachable
    }

    /// <summary>
    /// Authorization callback for debug purposes.
    /// </summary>
    public async System.Threading.Tasks.Task<IAuthTestClient.CallbackClientResponse> Callback(
        string code,
        string scope,
        string iss,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = Callback_CreateRequest(
            code,
            scope,
            iss
        );

        return await Callback(request, cancellationToken);
    }
    /// <summary>
    /// Sign in.
    /// </summary>
    public async System.Threading.Tasks.Task<IAuthTestClient.SignInClientResponse> SignIn(
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

            return IAuthTestClient.SignInClientResponse.Create204(response.StatusCode, response.Headers);
        }
        if ((int)response.StatusCode == 400 || 400 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 400, but content is null.");

            return IAuthTestClient.SignInClientResponse.Create400(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 401 || 401 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 401, but content is null.");

            return IAuthTestClient.SignInClientResponse.Create401(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == Default || Default == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is Default, but content is null.");

            return IAuthTestClient.SignInClientResponse.CreateDefault(response.StatusCode, response.Headers, content);
        }

#pragma warning disable CS0162 // Code is unreachable
        throw new System.InvalidOperationException("Code is unreachable.");
#pragma warning restore CS0162 // Code is unreachable
    }

    /// <summary>
    /// Sign in.
    /// </summary>
    public async System.Threading.Tasks.Task<IAuthTestClient.SignInClientResponse> SignIn(
        SignInRequest signInRequest,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = SignIn_CreateRequest(
            signInRequest
        );

        return await SignIn(request, cancellationToken);
    }

    private System.Net.Http.HttpRequestMessage Callback_CreateRequest(
        string code,
        string scope,
        string iss
    )
    {
        var path = "/identity-provider/v1/auth/callback";


        var uriBuilder = new System.Text.StringBuilder(path);

        var separator = '?';

        if (true)
        {
            uriBuilder
                .Append(separator)
                .Append("code=")
                .Append(UrlEncode(ValueToString(code)));
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
        if (true)
        {
            uriBuilder
                .Append(separator)
                .Append("iss=")
                .Append(UrlEncode(ValueToString(iss)));
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

    private System.Net.Http.HttpRequestMessage SignIn_CreateRequest(
        SignInRequest signInRequest
    )
    {
        var path = "/identity-provider/v1/auth/sign-in";


        var uriBuilder = new System.Text.StringBuilder(path);


        var uri = uriBuilder.ToString();
        System.Net.Http.HttpRequestMessage? request = null;
        try
        {
            request = new System.Net.Http.HttpRequestMessage(POST, uri);

            var content = true
                ? System.Net.Http.Json.JsonContent.Create(signInRequest, options: _jsonSerializerOptions)
                : null;
            request.Content = content;


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
    /// Authorization callback for debug purposes.
    /// </summary>
    System.Threading.Tasks.Task<CallbackClientResponse> Callback(
        string code,
        string scope,
        string iss,
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Sign in.
    /// </summary>
    System.Threading.Tasks.Task<SignInClientResponse> SignIn(
        SignInRequest signInRequest,
        System.Threading.CancellationToken cancellationToken = default
    );

    public class CallbackClientResponse
    {
        private readonly AuthorizationCallbackParameters? _content200;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content400;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _contentDefault;

        private CallbackClientResponse(
            System.Net.HttpStatusCode statusCode,
            System.Net.Http.Headers.HttpResponseHeaders headers,
            AuthorizationCallbackParameters? content200 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content400 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? contentDefault = null
        )
        {
            StatusCode = statusCode;
            Headers = headers;
            _content200 = content200;
            _content400 = content400;
            _contentDefault = contentDefault;
        }

        public System.Net.HttpStatusCode StatusCode { get; }

        public System.Net.Http.Headers.HttpResponseHeaders Headers { get; }

        public AuthorizationCallbackParameters Content200
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

        public static CallbackClientResponse Create200(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, AuthorizationCallbackParameters content)
        {
            if ((int)code != 200 && 200 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 200 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new CallbackClientResponse(statusCode: code, headers: headers, content200: content);
        }

        public static CallbackClientResponse Create400(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 400 && 400 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 400 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new CallbackClientResponse(statusCode: code, headers: headers, content400: content);
        }

        public static CallbackClientResponse CreateDefault(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code Default is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new CallbackClientResponse(statusCode: code, headers: headers, contentDefault: content);
        }

    }
    public class SignInClientResponse
    {
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content400;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content401;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _contentDefault;

        private SignInClientResponse(
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

        public static SignInClientResponse Create204(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers)
        {
            if ((int)code != 204 && 204 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 204 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new SignInClientResponse(statusCode: code, headers: headers);
        }

        public static SignInClientResponse Create400(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 400 && 400 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 400 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new SignInClientResponse(statusCode: code, headers: headers, content400: content);
        }

        public static SignInClientResponse Create401(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 401 && 401 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 401 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new SignInClientResponse(statusCode: code, headers: headers, content401: content);
        }

        public static SignInClientResponse CreateDefault(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code Default is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new SignInClientResponse(statusCode: code, headers: headers, contentDefault: content);
        }

    }
}

#nullable restore
