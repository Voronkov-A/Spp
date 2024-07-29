#nullable enable

namespace Spp.IdentityProvider.TestClient.Applications.V1;

public partial class ApplicationsTestClient : IApplicationsTestClient
{
    private const int Default = -1;
    private static readonly System.Net.Http.HttpMethod DELETE = System.Net.Http.HttpMethod.Delete;
    private static readonly System.Net.Http.HttpMethod GET = System.Net.Http.HttpMethod.Get;
    private static readonly System.Net.Http.HttpMethod POST = System.Net.Http.HttpMethod.Post;
    private static readonly System.Net.Http.HttpMethod PUT = System.Net.Http.HttpMethod.Put;

    private readonly System.Net.Http.HttpClient _httpClient;
    private readonly System.Text.Json.JsonSerializerOptions _jsonSerializerOptions;

    public ApplicationsTestClient(System.Net.Http.HttpClient httpClient, System.Text.Json.JsonSerializerOptions jsonSerializerOptions)
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
    /// Create application.
    /// </summary>
    public async System.Threading.Tasks.Task<IApplicationsTestClient.CreateClientResponse> Create(
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
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<CreateApplicationResponse?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 201, but content is null.");

            return IApplicationsTestClient.CreateClientResponse.Create201(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 400 || 400 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 400, but content is null.");

            return IApplicationsTestClient.CreateClientResponse.Create400(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 401 || 401 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 401, but content is null.");

            return IApplicationsTestClient.CreateClientResponse.Create401(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 403 || 403 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 403, but content is null.");

            return IApplicationsTestClient.CreateClientResponse.Create403(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == Default || Default == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is Default, but content is null.");

            return IApplicationsTestClient.CreateClientResponse.CreateDefault(response.StatusCode, response.Headers, content);
        }

#pragma warning disable CS0162 // Code is unreachable
        throw new System.InvalidOperationException("Code is unreachable.");
#pragma warning restore CS0162 // Code is unreachable
    }

    /// <summary>
    /// Create application.
    /// </summary>
    public async System.Threading.Tasks.Task<IApplicationsTestClient.CreateClientResponse> Create(
        CreateApplicationRequest createApplicationRequest,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = Create_CreateRequest(
            createApplicationRequest
        );

        return await Create(request, cancellationToken);
    }
    /// <summary>
    /// List applications.
    /// </summary>
    public async System.Threading.Tasks.Task<IApplicationsTestClient.ListClientResponse> List(
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
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<ApplicationViewList?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 200, but content is null.");

            return IApplicationsTestClient.ListClientResponse.Create200(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 400 || 400 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 400, but content is null.");

            return IApplicationsTestClient.ListClientResponse.Create400(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 401 || 401 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 401, but content is null.");

            return IApplicationsTestClient.ListClientResponse.Create401(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 403 || 403 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 403, but content is null.");

            return IApplicationsTestClient.ListClientResponse.Create403(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == Default || Default == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is Default, but content is null.");

            return IApplicationsTestClient.ListClientResponse.CreateDefault(response.StatusCode, response.Headers, content);
        }

#pragma warning disable CS0162 // Code is unreachable
        throw new System.InvalidOperationException("Code is unreachable.");
#pragma warning restore CS0162 // Code is unreachable
    }

    /// <summary>
    /// List applications.
    /// </summary>
    public async System.Threading.Tasks.Task<IApplicationsTestClient.ListClientResponse> List(
        string clientId,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = List_CreateRequest(
            clientId
        );

        return await List(request, cancellationToken);
    }
    /// <summary>
    /// Update application.
    /// </summary>
    public async System.Threading.Tasks.Task<IApplicationsTestClient.UpdateClientResponse> Update(
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

            return IApplicationsTestClient.UpdateClientResponse.Create204(response.StatusCode, response.Headers);
        }
        if ((int)response.StatusCode == 400 || 400 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 400, but content is null.");

            return IApplicationsTestClient.UpdateClientResponse.Create400(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 401 || 401 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 401, but content is null.");

            return IApplicationsTestClient.UpdateClientResponse.Create401(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 403 || 403 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 403, but content is null.");

            return IApplicationsTestClient.UpdateClientResponse.Create403(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == 404 || 404 == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is 404, but content is null.");

            return IApplicationsTestClient.UpdateClientResponse.Create404(response.StatusCode, response.Headers, content);
        }
        if ((int)response.StatusCode == Default || Default == -1)
        {
            var content = await System.Net.Http.Json.HttpContentJsonExtensions.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails?>(
                response.Content,
                _jsonSerializerOptions,
                cancellationToken
            ) ?? throw new System.InvalidOperationException("Code is Default, but content is null.");

            return IApplicationsTestClient.UpdateClientResponse.CreateDefault(response.StatusCode, response.Headers, content);
        }

#pragma warning disable CS0162 // Code is unreachable
        throw new System.InvalidOperationException("Code is unreachable.");
#pragma warning restore CS0162 // Code is unreachable
    }

    /// <summary>
    /// Update application.
    /// </summary>
    public async System.Threading.Tasks.Task<IApplicationsTestClient.UpdateClientResponse> Update(
        string id,
        UpdateApplicationRequest updateApplicationRequest,
        System.Threading.CancellationToken cancellationToken = default
    )
    {
        using var request = Update_CreateRequest(
            id,
            updateApplicationRequest
        );

        return await Update(request, cancellationToken);
    }

    private System.Net.Http.HttpRequestMessage Create_CreateRequest(
        CreateApplicationRequest createApplicationRequest
    )
    {
        var path = "/identity-provider/v1/applications";


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
        var path = "/identity-provider/v1/applications";


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

    ~ApplicationsTestClient()
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

public partial interface IApplicationsTestClient : System.IDisposable
{
    private const int Default = -1;

    /// <summary>
    /// Create application.
    /// </summary>
    System.Threading.Tasks.Task<CreateClientResponse> Create(
        CreateApplicationRequest createApplicationRequest,
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// List applications.
    /// </summary>
    System.Threading.Tasks.Task<ListClientResponse> List(
        string clientId,
        System.Threading.CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Update application.
    /// </summary>
    System.Threading.Tasks.Task<UpdateClientResponse> Update(
        string id,
        UpdateApplicationRequest updateApplicationRequest,
        System.Threading.CancellationToken cancellationToken = default
    );

    public class CreateClientResponse
    {
        private readonly CreateApplicationResponse? _content201;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content400;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content401;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content403;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _contentDefault;

        private CreateClientResponse(
            System.Net.HttpStatusCode statusCode,
            System.Net.Http.Headers.HttpResponseHeaders headers,
            CreateApplicationResponse? content201 = null,
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

        public CreateApplicationResponse Content201
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

        public static CreateClientResponse Create201(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, CreateApplicationResponse content)
        {
            if ((int)code != 201 && 201 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 201 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new CreateClientResponse(statusCode: code, headers: headers, content201: content);
        }

        public static CreateClientResponse Create400(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 400 && 400 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 400 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new CreateClientResponse(statusCode: code, headers: headers, content400: content);
        }

        public static CreateClientResponse Create401(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 401 && 401 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 401 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new CreateClientResponse(statusCode: code, headers: headers, content401: content);
        }

        public static CreateClientResponse Create403(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 403 && 403 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 403 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new CreateClientResponse(statusCode: code, headers: headers, content403: content);
        }

        public static CreateClientResponse CreateDefault(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code Default is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new CreateClientResponse(statusCode: code, headers: headers, contentDefault: content);
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
    public class ListClientResponse
    {
        private readonly ApplicationViewList? _content200;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content400;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content401;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content403;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _contentDefault;

        private ListClientResponse(
            System.Net.HttpStatusCode statusCode,
            System.Net.Http.Headers.HttpResponseHeaders headers,
            ApplicationViewList? content200 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content400 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content401 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? content403 = null,
            Microsoft.AspNetCore.Mvc.ProblemDetails? contentDefault = null
        )
        {
            StatusCode = statusCode;
            Headers = headers;
            _content200 = content200;
            _content400 = content400;
            _content401 = content401;
            _content403 = content403;
            _contentDefault = contentDefault;
        }

        public System.Net.HttpStatusCode StatusCode { get; }

        public System.Net.Http.Headers.HttpResponseHeaders Headers { get; }

        public ApplicationViewList Content200
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

        public static ListClientResponse Create200(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, ApplicationViewList content)
        {
            if ((int)code != 200 && 200 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 200 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new ListClientResponse(statusCode: code, headers: headers, content200: content);
        }

        public static ListClientResponse Create400(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 400 && 400 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 400 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new ListClientResponse(statusCode: code, headers: headers, content400: content);
        }

        public static ListClientResponse Create401(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 401 && 401 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 401 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new ListClientResponse(statusCode: code, headers: headers, content401: content);
        }

        public static ListClientResponse Create403(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 403 && 403 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 403 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new ListClientResponse(statusCode: code, headers: headers, content403: content);
        }

        public static ListClientResponse CreateDefault(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code Default is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new ListClientResponse(statusCode: code, headers: headers, contentDefault: content);
        }

    }
    public class UpdateClientResponse
    {
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content400;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content401;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content403;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _content404;
        private readonly Microsoft.AspNetCore.Mvc.ProblemDetails? _contentDefault;

        private UpdateClientResponse(
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

        public static UpdateClientResponse Create204(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers)
        {
            if ((int)code != 204 && 204 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 204 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new UpdateClientResponse(statusCode: code, headers: headers);
        }

        public static UpdateClientResponse Create400(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 400 && 400 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 400 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new UpdateClientResponse(statusCode: code, headers: headers, content400: content);
        }

        public static UpdateClientResponse Create401(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 401 && 401 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 401 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new UpdateClientResponse(statusCode: code, headers: headers, content401: content);
        }

        public static UpdateClientResponse Create403(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 403 && 403 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 403 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new UpdateClientResponse(statusCode: code, headers: headers, content403: content);
        }

        public static UpdateClientResponse Create404(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != 404 && 404 != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code 404 is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new UpdateClientResponse(statusCode: code, headers: headers, content404: content);
        }

        public static UpdateClientResponse CreateDefault(System.Net.HttpStatusCode code, System.Net.Http.Headers.HttpResponseHeaders headers, Microsoft.AspNetCore.Mvc.ProblemDetails content)
        {
            if ((int)code != Default && Default != -1)
#pragma warning disable CS0162 // Code is unreachable
            {
                throw new System.InvalidOperationException("Status code Default is expected.");
            }
#pragma warning restore CS0162 // Code is unreachable

            return new UpdateClientResponse(statusCode: code, headers: headers, contentDefault: content);
        }

    }
}

#nullable restore
