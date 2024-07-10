using Microsoft.Extensions.Options;
using Spp.Common.EventSourcing.EventStore.Postgres;
using Spp.Common.Postgres;

namespace Spp.Authorization.Persistence.Schemas.Indices;

public class IndicesDatabase(
    IConnectionFactory connectionFactory,
    IOptions<PostgresEventStoreConnectionSettings> settings) :
    PostgresDatabase(connectionFactory, settings.Value);
