using Spp.Common.Migrations;
using Spp.Common.Migrations.Postgres;

namespace Spp.Authorization.Persistence.Schemas.Indices.Migrations;

public class _0001_Initialize(IndicesDatabase database) :
    BasePostgresMigration(database),
    IMigration<IndicesDatabase>
{
    public override int Index => 1;

    protected override string UpSql => """
create schema if not exists indices;

create function indices.GetGeneratedUserNameStem(text) returns text language sql immutable as
$$
    select (regexp_matches($1, E'^(\\* [^ ]+ [^ ]+)(.*)$')::text[])[1];
$$;

create table indices.FirstNames (
    GeneratedUserNamePartId text not null,
    Index bigint not null,
    constraint PK_FirstNames primary key (Index)
);

create table indices.LastNames (
    GeneratedUserNamePartId text not null,
    Index bigint not null,
    constraint PK_LastNames primary key (Index)
);

create table indices.RoleIsDefaults (
    RoleId text not null,
    IsDefault boolean not null,
    constraint PK_RoleIsDefaults primary key (RoleId)
);

create index IX_RoleIsDefaults_IsDefault on indices.RoleIsDefaults (IsDefault);

create table indices.UserIdentities (
    UserId text not null,
    ProviderId text not null,
    SubjectId text not null,
    constraint PK_UserIdentities primary key (ProviderId, SubjectId)
);

create table indices.UserNames (
    UserId text not null,
    Value text not null,
    constraint PK_UserNames primary key (Value)
);

create index IX_UserNames_Value on indices.UserNames (
    coalesce(indices.GetGeneratedUserNameStem(Value), Value),
    length(Value) desc,
    Value desc
);
""";
}
