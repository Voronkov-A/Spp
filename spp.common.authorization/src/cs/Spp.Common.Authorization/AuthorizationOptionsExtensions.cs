using Microsoft.AspNetCore.Authorization;

namespace Spp.Common.Authorization;

public static class AuthorizationOptionsExtensions
{
    public static AuthorizationOptions AddClientOnlyPolicy(this AuthorizationOptions options, string name)
    {
        options.AddPolicy(name, c => c.RequireAssertion(ctx => ctx.User.IsClient()));
        return options;
    }

    public static AuthorizationOptions AddUserOnlyPolicy(this AuthorizationOptions options, string name)
    {
        options.AddPolicy(name, c => c.RequireAssertion(ctx => ctx.User.IsUser()));
        return options;
    }

    public static AuthorizationOptions AddPermissionPolicy(
        this AuthorizationOptions options,
        string name,
        string permission)
    {
        options.AddPolicy(name, c => c.AddRequirements(new PermissionRequirement(permission)));
        return options;
    }
}
