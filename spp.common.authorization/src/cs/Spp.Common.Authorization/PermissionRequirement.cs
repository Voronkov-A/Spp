using Microsoft.AspNetCore.Authorization;

namespace Spp.Common.Authorization;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}
