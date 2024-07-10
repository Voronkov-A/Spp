namespace Spp.Common.Domain;

public interface IEventDispatcher
{
    void Dispatch(object aggregate, object evt);
}
