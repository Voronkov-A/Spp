#nullable enable

namespace Spp.Authorization.Client.Auth.V1;

public partial class RbacClient : IRbacClient, System.IDisposable
{
    private static readonly System.Net.Http.HttpMethod DELETE = System.Net.Http.HttpMethod.Delete;
    private static readonly System.Net.Http.HttpMethod GET = System.Net.Http.HttpMethod.Get;
    private static readonly System.Net.Http.HttpMethod POST = System.Net.Http.HttpMethod.Post;
    private static readonly System.Net.Http.HttpMethod PUT = System.Net.Http.HttpMethod.Put;

    private readonly System.Net.Http.HttpClient _httpClient;
    private readonly System.Text.Json.JsonSerializerOptions _jsonSerializerOptions;

    public RbacClient(System.Net.Http.HttpClient httpClient, Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Http.Json.JsonOptions> jsonOptions)
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
    /// Create or update permission group.
    /// </summary>
    public async System.Threading.Tasks.Task CreateOrUpdatePermissionGroup(
        string id,
        CreateOrUpdatePermissionGroupRequest createOrUpdatePermissionGroupRequest,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = CreateOrUpdatePermissionGroup_CreateRequest(
            id,
            createOrUpdatePermissionGroupRequest
        );

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode != 204)
        {
            throw new System.InvalidOperationException("Expected status code 204, but the server responded with " + response.StatusCode.ToString() + ".");
        }

    }

    /// <summary>
    /// Create role.
    /// </summary>
    public async System.Threading.Tasks.Task<CreateRoleResponse> CreateRole(
        CreateRoleRequest createRoleRequest,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = CreateRole_CreateRequest(
            createRoleRequest
        );

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode != 201)
        {
            throw new System.InvalidOperationException("Expected status code 201, but the server responded with " + response.StatusCode.ToString() + ".");
        }

        var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<CreateRoleResponse?>(
            response.Content,
            _jsonSerializerOptions,
            cancellationToken);
        return content ?? throw new System.InvalidOperationException("Code is 201, but result is null.");
    }

    /// <summary>
    /// Delete role.
    /// </summary>
    public async System.Threading.Tasks.Task DeleteRole(
        string id,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = DeleteRole_CreateRequest(
            id
        );

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode != 204)
        {
            throw new System.InvalidOperationException("Expected status code 204, but the server responded with " + response.StatusCode.ToString() + ".");
        }

    }


    private System.Net.Http.HttpRequestMessage CreateOrUpdatePermissionGroup_CreateRequest(
        string id,
        CreateOrUpdatePermissionGroupRequest createOrUpdatePermissionGroupRequest
    )
    {
        var path = "/v1/rbac/permission-groups/{id}";

        path = path.Replace("{" + "id" + "}", id.ToString());

        var uriBuilder = new System.Text.StringBuilder(path);


        var uri = uriBuilder.ToString();
        System.Net.Http.HttpRequestMessage? request = null;
        try
        {
            request = new System.Net.Http.HttpRequestMessage(PUT, uri);

            var content = true
                ? System.Net.Http.Json.JsonContent.Create(createOrUpdatePermissionGroupRequest, options: _jsonSerializerOptions)
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

    private System.Net.Http.HttpRequestMessage CreateRole_CreateRequest(
        CreateRoleRequest createRoleRequest
    )
    {
        var path = "/v1/rbac/roles";


        var uriBuilder = new System.Text.StringBuilder(path);


        var uri = uriBuilder.ToString();
        System.Net.Http.HttpRequestMessage? request = null;
        try
        {
            request = new System.Net.Http.HttpRequestMessage(POST, uri);

            var content = true
                ? System.Net.Http.Json.JsonContent.Create(createRoleRequest, options: _jsonSerializerOptions)
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

    private System.Net.Http.HttpRequestMessage DeleteRole_CreateRequest(
        string id
    )
    {
        var path = "/v1/rbac/roles/{id}";

        path = path.Replace("{" + "id" + "}", id.ToString());

        var uriBuilder = new System.Text.StringBuilder(path);


        var uri = uriBuilder.ToString();
        System.Net.Http.HttpRequestMessage? request = null;
        try
        {
            request = new System.Net.Http.HttpRequestMessage(DELETE, uri);



            return request;
        }
        catch
        {
            request?.Dispose();
            throw;
        }
    }

    ~RbacClient()
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

public partial interface IRbacClient
{
    /// <summary>
    /// Create or update permission group.
    /// </summary>
    System.Threading.Tasks.Task CreateOrUpdatePermissionGroup(
        string id,
        CreateOrUpdatePermissionGroupRequest createOrUpdatePermissionGroupRequest,
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Create role.
    /// </summary>
    System.Threading.Tasks.Task<CreateRoleResponse> CreateRole(
        CreateRoleRequest createRoleRequest,
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Delete role.
    /// </summary>
    System.Threading.Tasks.Task DeleteRole(
        string id,
        System.Threading.CancellationToken cancellationToken = default
    );

}

#nullable restore
