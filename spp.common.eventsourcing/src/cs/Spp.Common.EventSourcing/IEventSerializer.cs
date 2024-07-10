namespace Spp.Common.EventSourcing;

public interface IEventSerializer
{
    byte[] Serialize(string type, object evt);

    object Deserialize(string type, byte[] bytes);
}
