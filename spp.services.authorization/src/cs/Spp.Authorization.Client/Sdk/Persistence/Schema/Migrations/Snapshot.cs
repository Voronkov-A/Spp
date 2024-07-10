namespace Spp.Authorization.Client.Sdk.Persistence.Schema.Migrations;

public class Snapshot(AuthorizationDatabase database)
{
    public string Up { get; } = $"""
create schema if not exists {database.SchemaName};

create table {database.SchemaName}.RolePermissions (
    RoleId text not null,
    PermissionGroupId text not null,
    PermissionId text not null,
    constraint PK_RolePermissions primary key (RoleId, PermissionGroupId, PermissionId)
);
""";
}
