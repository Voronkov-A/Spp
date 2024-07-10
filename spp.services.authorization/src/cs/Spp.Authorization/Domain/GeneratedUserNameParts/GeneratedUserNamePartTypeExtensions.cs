using System;

namespace Spp.Authorization.Domain.GeneratedUserNameParts;

public static class GeneratedUserNamePartTypeExtensions
{
    public static GeneratedUserNamePartType ToDomain(this Events.GeneratedUserNameParts.GeneratedUserNamePartType evt)
    {
        return evt switch
        {
            Events.GeneratedUserNameParts.GeneratedUserNamePartType.FirstName => GeneratedUserNamePartType.FirstName,
            Events.GeneratedUserNameParts.GeneratedUserNamePartType.LastName => GeneratedUserNamePartType.LastName,
            _ => throw new NotSupportedException(
                $"{nameof(Events.GeneratedUserNameParts.GeneratedUserNamePartType)}.{evt} is not supported.")
        };
    }

    public static Events.GeneratedUserNameParts.GeneratedUserNamePartType ToEvent(this GeneratedUserNamePartType evt)
    {
        return evt switch
        {
            GeneratedUserNamePartType.FirstName => Events.GeneratedUserNameParts.GeneratedUserNamePartType.FirstName,
            GeneratedUserNamePartType.LastName => Events.GeneratedUserNameParts.GeneratedUserNamePartType.LastName,
            _ => throw new NotSupportedException($"{nameof(GeneratedUserNamePartType)}.{evt} is not supported.")
        };
    }
}
