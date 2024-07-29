#nullable enable

namespace Spp.IdentityProvider.TestClient.Service.V1;

public partial class ServiceTestClient : IServiceTestClient
{
    private const int Default = -1;
    private static readonly System.Net.Http.HttpMethod DELETE = System.Net.Http.HttpMethod.Delete;
    private static readonly System.Net.Http.HttpMethod GET = System.Net.Http.HttpMethod.Get;
    private static readonly System.Net.Http.HttpMethod POST = System.Net.Http.HttpMethod.Post;
    private static readonly System.Net.Http.HttpMethod PUT = System.Net.Http.HttpMethod.Put;

    private readonly System.Net.Http.HttpClient _httpClient;
    private readonly System.Text.Json.JsonSerializerOptions _jsonSerializerOptions;

    public ServiceTestClient(System.Net.Http.HttpClient httpClient, System.Text.Json.JsonSerializerOptions jsonSerializerOptions)
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
    /// Check health.
    /// </summary>
    public async System.Threading.Tasks.Task<IServiceTestClient.HealthCheckClientResponse> HealthCheck(
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
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<HealthStatus?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 200, but content is null.");

            return IServiceTestClient.HealthCheckClientResponse.Create200(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == Default || Default == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is Default, but content is null.");

            return IServiceTestClient.HealthCheckClientResponse.CreateDefault(response.StatusCode, response.Headers, content);
        }

#pragma warning disable CS0162 // Code is unreachable
        throw new System.InvalidOperationException("Code is unreachable.");
#pragma warning restore CS0162 // Code is unreachable
    }

    /// <summary>
    /// Check health.
    /// </summary>
    public async System.Threading.Tasks.Task<IServiceTestClient.HealthCheckClientResponse> HealthCheck(
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = HealthCheck_CreateRequest(
        );

        return await HealthCheck(request, cancellationToken);
    }

    private System.Net.Http.HttpRequestMessage HealthCheck_CreateRequest(
    )
    {
        var path = "/identity-provider/v1/service/health";


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

    ~ServiceTestClient()
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

public partial interface IServiceTestClient : System.IDisposable
{
    private const int Default = -1;

    /// <summary>
    /// Check health.
    /// </summary>
    System.Threading.Tasks.Task<HealthCheckClientResponse> HealthCheck(
        System.Threading.CancellationToken cancellationToken = default
    );

    public class HealthCheckClientResponse
    {
        private readonly HealthStatus? _content200;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _contentDefault;

        private HealthCheckClientResponse(
            System.Net.HttpStatusCode statusCode,
            System.Net.Http.Headers.HttpResponseHeaders headers,
            HealthStatus? content200 = null,
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

        public HealthStatus Content200
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

        public static HealthCheckClientResponse Create200(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, HealthStatus content)
        {
            if ((int)code != 200 && 200 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 200 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new HealthCheckClientResponse(statusCode: code, headers: headers, content200: content);
        }

        public static HealthCheckClientResponse CreateDefault(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code Default is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new HealthCheckClientResponse(statusCode: code, headers: headers, contentDefault: content);
        }

    }
}

#nullable restore
