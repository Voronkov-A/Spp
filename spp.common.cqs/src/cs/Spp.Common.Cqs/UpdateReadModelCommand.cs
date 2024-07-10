using Spp.Common.Subscriptions;

namespace Spp.Common.Cqs;

public class UpdateReadModelCommand<TEvent>(EventEnvelope<TEvent> envelope) : HandleEventCommand<TEvent>(envelope);

public static class UpdateReadModelCommand
{
    public static UpdateReadModelCommand<TEvent> Create<TEvent>(EventEnvelope<TEvent> envelope)
    {
        return new UpdateReadModelCommand<TEvent>(envelope);
    }
}
