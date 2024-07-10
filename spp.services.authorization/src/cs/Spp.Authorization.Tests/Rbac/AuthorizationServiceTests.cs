using AutoFixture;
using IntegrationMocks.Core;
using Spp.Authorization.TestClient.Rbac.V1;
using Spp.Authorization.Tests.TestHelpers.Collections;
using Spp.Authorization.Tests.TestHelpers.Fixtures;
using Spp.Authorization.Tests.TestHelpers.Services;
using System.Net;
using System.Threading.Tasks;
using Spp.Common.TestHelpers.Http;
using Xunit;
using Spp.Authorization.Tests.TestHelpers;
using Spp.Authorization.Tests.TestHelpers.Factories.TestClient;
using System.Linq;
using Spp.Authorization.Tests.TestHelpers.Customizations;
using Spp.Authorization.TestClient.Auth.V1;
using Spp.Authorization.WebApi.Common.V1;
using Spp.Common.Miscellaneous;
using Spp.Authorization.TestClient.Users.V1;
using System.Threading;
using System;

namespace Spp.Authorization.Tests.Rbac;

[Collection(nameof(AuthorizationServiceCollection))]
public class AuthorizationServiceTests
{
    private readonly IFixture _fixture;
    private readonly IInfrastructureService<AuthorizationContract> _authorization;
    private readonly Authenticator _authenticator;
    private readonly RbacFactory _rbacFactory;

    public AuthorizationServiceTests(AuthorizationServiceFixture authorizationServiceFixture)
    {
        _fixture = new Fixture().Customize(new TranslationCustomization());
        _authorization = authorizationServiceFixture.Authorization;
        _authenticator = new(authorizationServiceFixture.Authorization, authorizationServiceFixture.IdentityProvider);
        _rbacFactory = new RbacFactory(_fixture);
    }

    [Fact]
    public async Task Cannot_create_permission_group_when_unauthorized()
    {
        // ARRANGE
        using var client = new RbacTestClient(_authorization.Contract.WebApiUrl);
        var permissionGroupId = _fixture.Create<string>();
        var request = _fixture.Create<CreateOrUpdatePermissionGroupRequest>();

        // ACT
        var response = await client.CreateOrUpdatePermissionGroup(permissionGroupId, request);

        // ASSERT
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Can_create_permission_group_when_authorized_with_client_credentials()
    {
        // ARRANGE
        var accessToken = await _authenticator.GetClientAccessToken();
        using var client = new RbacTestClient(HttpClientFactory.Create(_authorization.Contract.WebApiUrl, accessToken));
        var permissionGroupId = _fixture.Create<string>();
        var request = _fixture.Create<CreateOrUpdatePermissionGroupRequest>();

        // ACT
        var response = await client.CreateOrUpdatePermissionGroup(permissionGroupId, request);

        // ASSERT
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Cannot_create_permission_group_when_authorized_with_superuser_credentials()
    {
        // ARRANGE
        var accessToken = await _authenticator.GetSuperuserAccessToken();
        using var client = new RbacTestClient(HttpClientFactory.Create(_authorization.Contract.WebApiUrl, accessToken));
        var permissionGroupId = _fixture.Create<string>();
        var request = _fixture.Create<CreateOrUpdatePermissionGroupRequest>();

        // ACT
        var response = await client.CreateOrUpdatePermissionGroup(permissionGroupId, request);

        // ASSERT
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Cannot_create_permission_group_when_authorized_with_user_credentials()
    {
        // ARRANGE
        var accessToken = await _authenticator.GetUserAccessToken();
        using var client = new RbacTestClient(HttpClientFactory.Create(_authorization.Contract.WebApiUrl, accessToken));
        var permissionGroupId = _fixture.Create<string>();
        var request = _fixture.Create<CreateOrUpdatePermissionGroupRequest>();

        // ACT
        var response = await client.CreateOrUpdatePermissionGroup(permissionGroupId, request);

        // ASSERT
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Cannot_create_role_when_unauthorized()
    {
        // ARRANGE
        using var client = new RbacTestClient(_authorization.Contract.WebApiUrl);
        var request = _rbacFactory.CreateRoleRequest(Enumerable.Empty<PermissionReference>());

        // ACT
        var response = await client.CreateRole(request);

        // ASSERT
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Can_create_role_when_authorized_with_client_credentials()
    {
        // ARRANGE
        var accessToken = await _authenticator.GetClientAccessToken();
        using var client = new RbacTestClient(HttpClientFactory.Create(_authorization.Contract.WebApiUrl, accessToken));
        var request = _rbacFactory.CreateRoleRequest(Enumerable.Empty<PermissionReference>());

        // ACT
        var response = await client.CreateRole(request);

        // ASSERT
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Can_create_role_when_authorized_with_superuser_credentials()
    {
        // ARRANGE
        var accessToken = await _authenticator.GetSuperuserAccessToken();
        using var client = new RbacTestClient(HttpClientFactory.Create(_authorization.Contract.WebApiUrl, accessToken));
        var request = _rbacFactory.CreateRoleRequest(Enumerable.Empty<PermissionReference>());

        // ACT
        var response = await client.CreateRole(request);

        // ASSERT
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Cannot_create_role_when_authorized_with_user_credentials()
    {
        // ARRANGE
        var accessToken = await _authenticator.GetUserAccessToken();
        using var client = new RbacTestClient(HttpClientFactory.Create(_authorization.Contract.WebApiUrl, accessToken));
        var request = _rbacFactory.CreateRoleRequest(Enumerable.Empty<PermissionReference>());

        // ACT
        var response = await client.CreateRole(request);

        // ASSERT
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Can_create_role_when_authorized_with_user_credentials_and_user_has_permission()
    {
        // ARRANGE
        var registrationAccessToken = await _authenticator.GetUserAccessToken();
        using var registrationClient = new AuthTestClient(HttpClientFactory.Create(
            _authorization.Contract.WebApiUrl,
            registrationAccessToken));
        var userId = (await registrationClient.GetUserInfo()).Content200.Id;

        var superuserAccessToken = await _authenticator.GetSuperuserAccessToken();

        using var superuserRbacClient = new RbacTestClient(HttpClientFactory.Create(
            _authorization.Contract.WebApiUrl,
            superuserAccessToken));
        var createRoleRequest = _rbacFactory.CreateRoleRequest(new[]
        {
            new PermissionReference("authorization", EnumSerializer.ToString(AuthorizationPermissionIds.ManageRoles))
        });
        string roleId;
        using (var cancellation = new CancellationTokenSource(TimeSpan.FromMinutes(1)))
        {
            var createRoleResponse = await Wait(
                async ct => await superuserRbacClient.CreateRole(createRoleRequest, ct),
                x => x.StatusCode == HttpStatusCode.Created,
                cancellation.Token);
            roleId = createRoleResponse.Content201.Id;
        }

        try
        {
            var superuserUsersClient = new UsersTestClient(HttpClientFactory.Create(
                _authorization.Contract.WebApiUrl,
                superuserAccessToken));
            await superuserUsersClient.AssignRole(userId, roleId);

            var accessToken = await _authenticator.GetUserAccessToken();
            using var client =
                new RbacTestClient(HttpClientFactory.Create(_authorization.Contract.WebApiUrl, accessToken));
            var request = _rbacFactory.CreateRoleRequest(Enumerable.Empty<PermissionReference>());

            // ACT
            var response = await client.CreateRole(request);

            // ASSERT
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
        finally
        {
            await superuserRbacClient.DeleteRole(roleId);
        }
    }

    private static async Task<T> Wait<T>(
        Func<CancellationToken, Task<T>> action,
        Func<T, bool> until,
        CancellationToken cancellationToken)
    {
        var period = TimeSpan.FromSeconds(10);

        while (true)
        {
            var result = await action(cancellationToken);

            if (until(result))
            {
                return result;
            }

            await Task.Delay(period, cancellationToken);
        }
    }
}
