using Microsoft.Extensions.Logging;
using Spp.Common.Migrations;
using System.Collections.Generic;

namespace Spp.Authorization.Persistence.Schemas.Indices;

public class IndicesMigrator(
    IMigrationStore<IndicesDatabase> migrationStore,
    IEnumerable<IMigration<IndicesDatabase>> migrations,
    ILogger<IndicesMigrator> logger) :
    Migrator(migrationStore, migrations, logger),
    IIndicesMigrator;
