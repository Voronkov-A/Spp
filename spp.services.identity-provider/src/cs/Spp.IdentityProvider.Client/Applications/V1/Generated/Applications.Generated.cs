#nullable enable

namespace Spp.IdentityProvider.Client.Applications.V1;

public partial class ApplicationsClient : IApplicationsClient, System.IDisposable
{
    private static readonly System.Net.Http.HttpMethod DELETE = System.Net.Http.HttpMethod.Delete;
    private static readonly System.Net.Http.HttpMethod GET = System.Net.Http.HttpMethod.Get;
    private static readonly System.Net.Http.HttpMethod POST = System.Net.Http.HttpMethod.Post;
    private static readonly System.Net.Http.HttpMethod PUT = System.Net.Http.HttpMethod.Put;

    private readonly System.Net.Http.HttpClient _httpClient;
    private readonly System.Text.Json.JsonSerializerOptions _jsonSerializerOptions;

    public ApplicationsClient(System.Net.Http.HttpClient httpClient, Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Http.Json.JsonOptions> jsonOptions)
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
    /// Create application.
    /// </summary>
    public async System.Threading.Tasks.Task<CreateApplicationResponse> Create(
        CreateApplicationRequest createApplicationRequest,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = Create_CreateRequest(
            createApplicationRequest
        );

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode != 201)
        {
            throw new System.InvalidOperationException("Expected status code 201, but the server responded with " + response.StatusCode.ToString() + ".");
        }

        var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<CreateApplicationResponse?>(
            response.Content,
            _jsonSerializerOptions,
            cancellationToken);
        return content ?? throw new System.InvalidOperationException("Code is 201, but result is null.");
    }

    /// <summary>
    /// List applications.
    /// </summary>
    public async System.Threading.Tasks.Task<ApplicationViewList> List(
        string clientId,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = List_CreateRequest(
            clientId
        );

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode != 200)
        {
            throw new System.InvalidOperationException("Expected status code 200, but the server responded with " + response.StatusCode.ToString() + ".");
        }

        var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<ApplicationViewList?>(
            response.Content,
            _jsonSerializerOptions,
            cancellationToken);
        return content ?? throw new System.InvalidOperationException("Code is 200, but result is null.");
    }

    /// <summary>
    /// Update application.
    /// </summary>
    public async System.Threading.Tasks.Task Update(
        string id,
        UpdateApplicationRequest updateApplicationRequest,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = Update_CreateRequest(
            id,
            updateApplicationRequest
        );

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode != 204)
        {
            throw new System.InvalidOperationException("Expected status code 204, but the server responded with " + response.StatusCode.ToString() + ".");
        }

    }


    private System.Net.Http.HttpRequestMessage Create_CreateRequest(
        CreateApplicationRequest createApplicationRequest
    )
    {
        var path = "/v1/applications";


        var uriBuilder = new System.Text.StringBuilder(path);


        var uri = uriBuilder.ToString();
        System.Net.Http.HttpRequestMessage? request = null;
        try
        {
            request = new System.Net.Http.HttpRequestMessage(POST, uri);

            var content = true
                ? System.Net.Http.Json.JsonContent.Create(createApplicationRequest, options: _jsonSerializerOptions)
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

    private System.Net.Http.HttpRequestMessage List_CreateRequest(
        string clientId
    )
    {
        var path = "/v1/applications";


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

    private System.Net.Http.HttpRequestMessage Update_CreateRequest(
        string id,
        UpdateApplicationRequest updateApplicationRequest
    )
    {
        var path = "/v1/applications/{id}";

        path = path.Replace("{" + "id" + "}", id.ToString());

        var uriBuilder = new System.Text.StringBuilder(path);


        var uri = uriBuilder.ToString();
        System.Net.Http.HttpRequestMessage? request = null;
        try
        {
            request = new System.Net.Http.HttpRequestMessage(PUT, uri);

            var content = true
                ? System.Net.Http.Json.JsonContent.Create(updateApplicationRequest, options: _jsonSerializerOptions)
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

    ~ApplicationsClient()
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

public partial interface IApplicationsClient
{
    /// <summary>
    /// Create application.
    /// </summary>
    System.Threading.Tasks.Task<CreateApplicationResponse> Create(
        CreateApplicationRequest createApplicationRequest,
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// List applications.
    /// </summary>
    System.Threading.Tasks.Task<ApplicationViewList> List(
        string clientId,
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Update application.
    /// </summary>
    System.Threading.Tasks.Task Update(
        string id,
        UpdateApplicationRequest updateApplicationRequest,
        System.Threading.CancellationToken cancellationToken = default
    );

}

#nullable restore
