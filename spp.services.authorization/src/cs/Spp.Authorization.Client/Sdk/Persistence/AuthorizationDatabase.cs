using Microsoft.Extensions.Options;
using Spp.Authorization.Client.Sdk.Settings;
using Spp.Common.Postgres;

namespace Spp.Authorization.Client.Sdk.Persistence;

public class AuthorizationDatabase(
    IConnectionFactory connectionFactory,
    IOptions<AuthorizationPersistenceSettings> settings,
    IOptions<AuthorizationPersistenceOptions> options) :
    PostgresDatabase(connectionFactory, settings.Value)
{
    public string SchemaName => options.Value.SchemaName;
}
