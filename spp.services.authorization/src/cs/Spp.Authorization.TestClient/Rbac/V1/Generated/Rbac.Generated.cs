#nullable enable

namespace Spp.Authorization.TestClient.Rbac.V1;

public partial class RbacTestClient : IRbacTestClient
{
    private const int Default = -1;
    private static readonly System.Net.Http.HttpMethod DELETE = System.Net.Http.HttpMethod.Delete;
    private static readonly System.Net.Http.HttpMethod GET = System.Net.Http.HttpMethod.Get;
    private static readonly System.Net.Http.HttpMethod POST = System.Net.Http.HttpMethod.Post;
    private static readonly System.Net.Http.HttpMethod PUT = System.Net.Http.HttpMethod.Put;

    private readonly System.Net.Http.HttpClient _httpClient;
    private readonly System.Text.Json.JsonSerializerOptions _jsonSerializerOptions;

    public RbacTestClient(System.Net.Http.HttpClient httpClient, System.Text.Json.JsonSerializerOptions jsonSerializerOptions)
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
    /// Create or update permission group.
    /// </summary>
    public async System.Threading.Tasks.Task<IRbacTestClient.CreateOrUpdatePermissionGroupClientResponse> CreateOrUpdatePermissionGroup(
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

            return IRbacTestClient.CreateOrUpdatePermissionGroupClientResponse.Create204(response.StatusCode, response.Headers);
        }
        if ((int)response.StatusCode == 400 || 400 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 400, but content is null.");

            return IRbacTestClient.CreateOrUpdatePermissionGroupClientResponse.Create400(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 401 || 401 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 401, but content is null.");

            return IRbacTestClient.CreateOrUpdatePermissionGroupClientResponse.Create401(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 403 || 403 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 403, but content is null.");

            return IRbacTestClient.CreateOrUpdatePermissionGroupClientResponse.Create403(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == Default || Default == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is Default, but content is null.");

            return IRbacTestClient.CreateOrUpdatePermissionGroupClientResponse.CreateDefault(response.StatusCode, response.Headers, content);
        }

#pragma warning disable CS0162 // Code is unreachable
        throw new System.InvalidOperationException("Code is unreachable.");
#pragma warning restore CS0162 // Code is unreachable
    }

    /// <summary>
    /// Create or update permission group.
    /// </summary>
    public async System.Threading.Tasks.Task<IRbacTestClient.CreateOrUpdatePermissionGroupClientResponse> CreateOrUpdatePermissionGroup(
        string id,
        CreateOrUpdatePermissionGroupRequest createOrUpdatePermissionGroupRequest,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = CreateOrUpdatePermissionGroup_CreateRequest(
            id,
            createOrUpdatePermissionGroupRequest
        );

        return await CreateOrUpdatePermissionGroup(request, cancellationToken);
    }
    /// <summary>
    /// Create role.
    /// </summary>
    public async System.Threading.Tasks.Task<IRbacTestClient.CreateRoleClientResponse> CreateRole(
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

        if ((int)response.StatusCode == 201 || 201 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<CreateRoleResponse?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 201, but content is null.");

            return IRbacTestClient.CreateRoleClientResponse.Create201(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 400 || 400 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 400, but content is null.");

            return IRbacTestClient.CreateRoleClientResponse.Create400(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 401 || 401 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 401, but content is null.");

            return IRbacTestClient.CreateRoleClientResponse.Create401(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 403 || 403 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 403, but content is null.");

            return IRbacTestClient.CreateRoleClientResponse.Create403(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == Default || Default == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is Default, but content is null.");

            return IRbacTestClient.CreateRoleClientResponse.CreateDefault(response.StatusCode, response.Headers, content);
        }

#pragma warning disable CS0162 // Code is unreachable
        throw new System.InvalidOperationException("Code is unreachable.");
#pragma warning restore CS0162 // Code is unreachable
    }

    /// <summary>
    /// Create role.
    /// </summary>
    public async System.Threading.Tasks.Task<IRbacTestClient.CreateRoleClientResponse> CreateRole(
        CreateRoleRequest createRoleRequest,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = CreateRole_CreateRequest(
            createRoleRequest
        );

        return await CreateRole(request, cancellationToken);
    }
    /// <summary>
    /// Delete role.
    /// </summary>
    public async System.Threading.Tasks.Task<IRbacTestClient.DeleteRoleClientResponse> DeleteRole(
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

            return IRbacTestClient.DeleteRoleClientResponse.Create204(response.StatusCode, response.Headers);
        }
        if ((int)response.StatusCode == 400 || 400 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 400, but content is null.");

            return IRbacTestClient.DeleteRoleClientResponse.Create400(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 401 || 401 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 401, but content is null.");

            return IRbacTestClient.DeleteRoleClientResponse.Create401(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 403 || 403 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 403, but content is null.");

            return IRbacTestClient.DeleteRoleClientResponse.Create403(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 404 || 404 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 404, but content is null.");

            return IRbacTestClient.DeleteRoleClientResponse.Create404(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == Default || Default == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is Default, but content is null.");

            return IRbacTestClient.DeleteRoleClientResponse.CreateDefault(response.StatusCode, response.Headers, content);
        }

#pragma warning disable CS0162 // Code is unreachable
        throw new System.InvalidOperationException("Code is unreachable.");
#pragma warning restore CS0162 // Code is unreachable
    }

    /// <summary>
    /// Delete role.
    /// </summary>
    public async System.Threading.Tasks.Task<IRbacTestClient.DeleteRoleClientResponse> DeleteRole(
        string id,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = DeleteRole_CreateRequest(
            id
        );

        return await DeleteRole(request, cancellationToken);
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

    ~RbacTestClient()
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

public partial interface IRbacTestClient : System.IDisposable
{
    private const int Default = -1;

    /// <summary>
    /// Create or update permission group.
    /// </summary>
    System.Threading.Tasks.Task<CreateOrUpdatePermissionGroupClientResponse> CreateOrUpdatePermissionGroup(
        string id,
        CreateOrUpdatePermissionGroupRequest createOrUpdatePermissionGroupRequest,
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Create role.
    /// </summary>
    System.Threading.Tasks.Task<CreateRoleClientResponse> CreateRole(
        CreateRoleRequest createRoleRequest,
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Delete role.
    /// </summary>
    System.Threading.Tasks.Task<DeleteRoleClientResponse> DeleteRole(
        string id,
        System.Threading.CancellationToken cancellationToken = default
    );

    public class CreateOrUpdatePermissionGroupClientResponse
    {
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content400;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content401;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content403;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _contentDefault;

        private CreateOrUpdatePermissionGroupClientResponse(
            System.Net.HttpStatusCode statusCode,
            System.Net.Http.Headers.HttpResponseHeaders headers,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content400 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content401 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content403 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? contentDefault = null
        )
        {
            StatusCode = statusCode;
            Headers = headers;
            _content400 = content400;
            _content401 = content401;
            _content403 = content403;
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

        public static CreateOrUpdatePermissionGroupClientResponse Create204(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers)
        {
            if ((int)code != 204 && 204 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 204 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new CreateOrUpdatePermissionGroupClientResponse(statusCode: code, headers: headers);
        }

        public static CreateOrUpdatePermissionGroupClientResponse Create400(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 400 && 400 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 400 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new CreateOrUpdatePermissionGroupClientResponse(statusCode: code, headers: headers, content400: content);
        }

        public static CreateOrUpdatePermissionGroupClientResponse Create401(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 401 && 401 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 401 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new CreateOrUpdatePermissionGroupClientResponse(statusCode: code, headers: headers, content401: content);
        }

        public static CreateOrUpdatePermissionGroupClientResponse Create403(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 403 && 403 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 403 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new CreateOrUpdatePermissionGroupClientResponse(statusCode: code, headers: headers, content403: content);
        }

        public static CreateOrUpdatePermissionGroupClientResponse CreateDefault(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code Default is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new CreateOrUpdatePermissionGroupClientResponse(statusCode: code, headers: headers, contentDefault: content);
        }

    }
    public class CreateRoleClientResponse
    {
        private readonly CreateRoleResponse? _content201;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content400;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content401;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content403;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _contentDefault;

        private CreateRoleClientResponse(
            System.Net.HttpStatusCode statusCode,
            System.Net.Http.Headers.HttpResponseHeaders headers,
            CreateRoleResponse? content201 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content400 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content401 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content403 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? contentDefault = null
        )
        {
            StatusCode = statusCode;
            Headers = headers;
            _content201 = content201;
            _content400 = content400;
            _content401 = content401;
            _content403 = content403;
            _contentDefault = contentDefault;
        }

        public System.Net.HttpStatusCode StatusCode { get; }

        public System.Net.Http.Headers.HttpResponseHeaders Headers { get; }

        public CreateRoleResponse Content201
        {
            get
            {
                if ((int)StatusCode != 201 && 201 != -1)
#pragma warning disable CS0162 // Code is unreachable
                {
                    throw new System.InvalidOperationException("Status code is " + StatusCode.ToString() + ", not 201.");
                }
#pragma warning restore CS0162 // Code is unreachable

                return _content201 ?? throw new System.InvalidOperationException("Status code is 201, but result is null.");
            }
        }

        public HeaderCollection201 Headers201 => new HeaderCollection201(Headers);

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

        public static CreateRoleClientResponse Create201(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, CreateRoleResponse content)
        {
            if ((int)code != 201 && 201 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 201 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new CreateRoleClientResponse(statusCode: code, headers: headers, content201: content);
        }

        public static CreateRoleClientResponse Create400(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 400 && 400 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 400 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new CreateRoleClientResponse(statusCode: code, headers: headers, content400: content);
        }

        public static CreateRoleClientResponse Create401(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 401 && 401 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 401 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new CreateRoleClientResponse(statusCode: code, headers: headers, content401: content);
        }

        public static CreateRoleClientResponse Create403(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 403 && 403 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 403 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new CreateRoleClientResponse(statusCode: code, headers: headers, content403: content);
        }

        public static CreateRoleClientResponse CreateDefault(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code Default is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new CreateRoleClientResponse(statusCode: code, headers: headers, contentDefault: content);
        }

        public readonly struct HeaderCollection201
        {
            private readonly System.Net.Http.Headers.HttpResponseHeaders _headers;

            public HeaderCollection201(System.Net.Http.Headers.HttpResponseHeaders headers)
            {
                _headers = headers;
            }

            public string? Location =>
                System.Linq.Enumerable.SingleOrDefault(_headers.GetValues("Location"));
        }
    }
    public class DeleteRoleClientResponse
    {
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content400;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content401;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content403;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content404;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _contentDefault;

        private DeleteRoleClientResponse(
            System.Net.HttpStatusCode statusCode,
            System.Net.Http.Headers.HttpResponseHeaders headers,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content400 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content401 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content403 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content404 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? contentDefault = null
        )
        {
            StatusCode = statusCode;
            Headers = headers;
            _content400 = content400;
            _content401 = content401;
            _content403 = content403;
            _content404 = content404;
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

        public static DeleteRoleClientResponse Create204(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers)
        {
            if ((int)code != 204 && 204 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 204 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new DeleteRoleClientResponse(statusCode: code, headers: headers);
        }

        public static DeleteRoleClientResponse Create400(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 400 && 400 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 400 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new DeleteRoleClientResponse(statusCode: code, headers: headers, content400: content);
        }

        public static DeleteRoleClientResponse Create401(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 401 && 401 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 401 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new DeleteRoleClientResponse(statusCode: code, headers: headers, content401: content);
        }

        public static DeleteRoleClientResponse Create403(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 403 && 403 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 403 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new DeleteRoleClientResponse(statusCode: code, headers: headers, content403: content);
        }

        public static DeleteRoleClientResponse Create404(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 404 && 404 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 404 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new DeleteRoleClientResponse(statusCode: code, headers: headers, content404: content);
        }

        public static DeleteRoleClientResponse CreateDefault(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code Default is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new DeleteRoleClientResponse(statusCode: code, headers: headers, contentDefault: content);
        }

    }
}

#nullable restore
