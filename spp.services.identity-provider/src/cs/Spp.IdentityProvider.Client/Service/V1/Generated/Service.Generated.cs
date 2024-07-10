#nullable enable

namespace Spp.IdentityProvider.Client.Service.V1;

public partial class ServiceClient : IServiceClient, System.IDisposable
{
    private static readonly System.Net.Http.HttpMethod DELETE = System.Net.Http.HttpMethod.Delete;
    private static readonly System.Net.Http.HttpMethod GET = System.Net.Http.HttpMethod.Get;
    private static readonly System.Net.Http.HttpMethod POST = System.Net.Http.HttpMethod.Post;
    private static readonly System.Net.Http.HttpMethod PUT = System.Net.Http.HttpMethod.Put;

    private readonly System.Net.Http.HttpClient _httpClient;
    private readonly System.Text.Json.JsonSerializerOptions _jsonSerializerOptions;

    public ServiceClient(System.Net.Http.HttpClient httpClient, Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Http.Json.JsonOptions> jsonOptions)
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
    /// Check health.
    /// </summary>
    public async System.Threading.Tasks.Task<HealthStatus> HealthCheck(
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = HealthCheck_CreateRequest(
        );

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode != 200)
        {
            throw new System.InvalidOperationException("Expected status code 200, but the server responded with " + response.StatusCode.ToString() + ".");
        }

        var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<HealthStatus?>(
            response.Content,
            _jsonSerializerOptions,
            cancellationToken);
        return content ?? throw new System.InvalidOperationException("Code is 200, but result is null.");
    }


    private System.Net.Http.HttpRequestMessage HealthCheck_CreateRequest(
    )
    {
        var path = "/v1/service/health";


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

    ~ServiceClient()
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

public partial interface IServiceClient
{
    /// <summary>
    /// Check health.
    /// </summary>
    System.Threading.Tasks.Task<HealthStatus> HealthCheck(
        System.Threading.CancellationToken cancellationToken = default
    );

}

#nullable restore
