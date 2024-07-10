namespace Spp.Authorization.Client.Sdk.Persistence;

internal class RolePermissionDto
{
    public required string RoleId { get; init; }

    public required string PermissionGroupId { get; init; }

    public required string PermissionId { get; init; }
}
