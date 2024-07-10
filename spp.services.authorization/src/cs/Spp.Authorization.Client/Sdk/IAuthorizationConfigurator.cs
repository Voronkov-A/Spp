using System;

namespace Spp.Authorization.Client.Sdk;

public interface IAuthorizationConfigurator
{
    IAuthorizationConfigurator WithPersistenceSchemaName(string schemaName);

    IAuthorizationConfigurator WithAuthorizationServiceSettingsSection(string sectionName);

    IAuthorizationConfigurator WithPersistenceSettingsSection(string sectionName);

    IAuthorizationConfigurator WithClientOnlyPolicy(string policyName);

    IAuthorizationConfigurator WithUserOnlyPolicy(string policyName);

    IAuthorizationConfigurator WithPermissionPolicy<TPermission>(string policyName, TPermission permission)
        where TPermission : struct, Enum;
}
