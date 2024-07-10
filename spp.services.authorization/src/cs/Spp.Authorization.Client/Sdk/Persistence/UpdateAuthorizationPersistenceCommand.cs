using Spp.Common.Cqs;
using Spp.Common.Subscriptions;

namespace Spp.Authorization.Client.Sdk.Persistence;

internal class UpdateAuthorizationPersistenceCommand<TEvent>(EventEnvelope<TEvent> envelope) :
    HandleEventCommand<TEvent>(envelope);
