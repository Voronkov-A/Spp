using Microsoft.Extensions.Logging;
using Spp.Common.Migrations;
using System.Collections.Generic;

namespace Spp.Authorization.Client.Sdk.Persistence.Schema;

public class AuthorizationMigrator(
    IMigrationStore<AuthorizationDatabase> migrationStore,
    IEnumerable<IMigration<AuthorizationDatabase>> migrations,
    ILogger<AuthorizationMigrator> logger) :
    Migrator(migrationStore, migrations, logger), IAuthorizationMigrator;
