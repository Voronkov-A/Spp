using Spp.Common.Migrations;
using Spp.Common.Migrations.Postgres;

namespace Spp.Authorization.Client.Sdk.Persistence.Schema.Migrations;

internal class _0001_Initialize(AuthorizationDatabase database) :
    BasePostgresMigration(database),
    IMigration<AuthorizationDatabase>
{
    public override int Index => 1;

    protected override string UpSql => $"""
create schema if not exists {database.SchemaName};

create table {database.SchemaName}.RolePermissions (
    RoleId text not null,
    PermissionGroupId text not null,
    PermissionId text not null,
    constraint PK_RolePermissions primary key (RoleId, PermissionGroupId, PermissionId)
);
""";
}
