using Spp.Common.Subscriptions;

namespace Spp.Common.Cqs;

public class UpdateIndexCommand<TEvent>(EventEnvelope<TEvent> envelope) : HandleEventCommand<TEvent>(envelope);

public static class UpdateIndexCommand
{
    public static UpdateIndexCommand<TEvent> Create<TEvent>(EventEnvelope<TEvent> envelope)
    {
        return new UpdateIndexCommand<TEvent>(envelope);
    }
}
