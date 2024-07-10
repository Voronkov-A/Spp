using Spp.Authorization.Domain.Common.Exceptions;
using Spp.Authorization.Events.GeneratedUserNameParts;
using Spp.Common.Domain;

namespace Spp.Authorization.Domain.GeneratedUserNameParts;

public class GeneratedUserNamePart : BaseAggregate
{
    public GeneratedUserNamePart(EntityId id, string value, GeneratedUserNamePartType type)
        : base(id, EventDispatcher<GeneratedUserNamePart>.Instance)
    {
        if (value.Length > 256)
        {
            throw new InvalidNameException("Value must not be longer than 256 symbols.", value);
        }

        if (value.Contains(' '))
        {
            throw new InvalidNameException("Value must not contain spaces.", value);
        }

        AddEvent(new GeneratedUserNamePartCreated(value: value, type: type.ToEvent()));
    }

    private GeneratedUserNamePart(EntityId id) : base(id, EventDispatcher<GeneratedUserNamePart>.Instance)
    {
    }

    public string Value { get; private set; } = null!;

    public GeneratedUserNamePartType Type { get; private set; }

    public bool IsDeleted { get; private set; }

    public void Delete()
    {
        if (IsDeleted)
        {
            return;
        }

        AddEvent(new GeneratedUserNamePartDeleted());
    }

    private void When(GeneratedUserNamePartCreated evt)
    {
        Value = evt.Value;
        Type = evt.Type.ToDomain();
    }

    private void When(GeneratedUserNamePartDeleted _)
    {
        IsDeleted = true;
    }
}
