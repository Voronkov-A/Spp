#nullable enable

namespace Spp.IdentityProvider.Client.Auth.V1;

public partial class AuthClient : IAuthClient, System.IDisposable
{
    private static readonly System.Net.Http.HttpMethod DELETE = System.Net.Http.HttpMethod.Delete;
    private static readonly System.Net.Http.HttpMethod GET = System.Net.Http.HttpMethod.Get;
    private static readonly System.Net.Http.HttpMethod POST = System.Net.Http.HttpMethod.Post;
    private static readonly System.Net.Http.HttpMethod PUT = System.Net.Http.HttpMethod.Put;

    private readonly System.Net.Http.HttpClient _httpClient;
    private readonly System.Text.Json.JsonSerializerOptions _jsonSerializerOptions;

    public AuthClient(System.Net.Http.HttpClient httpClient, Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Http.Json.JsonOptions> jsonOptions)
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
    /// Authorization callback for debug purposes.
    /// </summary>
    public async System.Threading.Tasks.Task<AuthorizationCallbackParameters> Callback(
        string code,
        string scope,
        string sessionState,
        string iss,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = Callback_CreateRequest(
            code,
            scope,
            sessionState,
            iss
        );

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode != 200)
        {
            throw new System.InvalidOperationException("Expected status code 200, but the server responded with " + response.StatusCode.ToString() + ".");
        }

        var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<AuthorizationCallbackParameters?>(
            response.Content,
            _jsonSerializerOptions,
            cancellationToken);
        return content ?? throw new System.InvalidOperationException("Code is 200, but result is null.");
    }

    /// <summary>
    /// Sign in.
    /// </summary>
    public async System.Threading.Tasks.Task SignIn(
        SignInRequest signInRequest,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = SignIn_CreateRequest(
            signInRequest
        );

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode != 204)
        {
            throw new System.InvalidOperationException("Expected status code 204, but the server responded with " + response.StatusCode.ToString() + ".");
        }

    }


    private System.Net.Http.HttpRequestMessage Callback_CreateRequest(
        string code,
        string scope,
        string sessionState,
        string iss
    )
    {
        var path = "/v1/auth/callback";


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
                .Append("session_state=")
                .Append(UrlEncode(ValueToString(sessionState)));
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
        var path = "/v1/auth/sign-in";


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

    ~AuthClient()
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

public partial interface IAuthClient
{
    /// <summary>
    /// Authorization callback for debug purposes.
    /// </summary>
    System.Threading.Tasks.Task<AuthorizationCallbackParameters> Callback(
        string code,
        string scope,
        string sessionState,
        string iss,
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Sign in.
    /// </summary>
    System.Threading.Tasks.Task SignIn(
        SignInRequest signInRequest,
        System.Threading.CancellationToken cancellationToken = default
    );

}

#nullable restore
