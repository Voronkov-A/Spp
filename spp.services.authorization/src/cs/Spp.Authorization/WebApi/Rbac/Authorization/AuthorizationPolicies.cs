namespace Spp.Authorization.WebApi.Rbac.Authorization;

public static class AuthorizationPolicies
{
    public const string ClientOnly = nameof(ClientOnly);

    public const string UserOnly = nameof(UserOnly);

    public const string ManageRoles = nameof(ManageRoles);

    public const string ManageUserRoles = nameof(ManageUserRoles);
}
