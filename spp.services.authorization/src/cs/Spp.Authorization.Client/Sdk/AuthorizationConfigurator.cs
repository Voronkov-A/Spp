using Microsoft.AspNetCore.Authorization;
using Spp.Authorization.Client.Sdk.Domain;
using Spp.Common.Authorization;
using Spp.Common.Domain;
using Spp.Common.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spp.Authorization.Client.Sdk;

internal class AuthorizationConfigurator(string serviceId) : IAuthorizationConfigurator
{
    private readonly HashSet<string> _permissions = new();
    private Action<AuthorizationOptions> _configure = delegate { };

    public string SchemaName { get; private set; } = "\"authorization\"";

    public string AuthorizationServiceSettingsSection { get; private set; } = "Authentication";

    public string PersistenceSettingsSection { get; private set; } = "Persistence:Connection";

    public PermissionGroupDefinition PermissionGroup => new(
        new EntityId(serviceId),
        _permissions.Select(x => new EntityId(x)));

    public IAuthorizationConfigurator WithPersistenceSchemaName(string schemaName)
    {
        SchemaName = schemaName;
        return this;
    }

    public IAuthorizationConfigurator WithAuthorizationServiceSettingsSection(string sectionName)
    {
        AuthorizationServiceSettingsSection = sectionName;
        return this;
    }

    public IAuthorizationConfigurator WithPersistenceSettingsSection(string sectionName)
    {
        PersistenceSettingsSection = sectionName;
        return this;
    }

    public IAuthorizationConfigurator WithClientOnlyPolicy(string policyName)
    {
        _configure += options => options.AddClientOnlyPolicy(policyName);
        return this;
    }

    public IAuthorizationConfigurator WithUserOnlyPolicy(string policyName)
    {
        _configure += options => options.AddUserOnlyPolicy(policyName);
        return this;
    }

    public IAuthorizationConfigurator WithPermissionPolicy<TPermission>(string policyName, TPermission permission)
        where TPermission : struct, Enum
    {
        var reference = new PermissionReference(
            new EntityId(serviceId),
            new EntityId(EnumSerializer.ToString(permission)))
            .ToString();
        _configure += options => options.AddPermissionPolicy(policyName, reference);
        _permissions.Add(EnumSerializer.ToString(permission));
        return this;
    }

    public void Configure(AuthorizationOptions options)
    {
        _configure(options);
    }
}
