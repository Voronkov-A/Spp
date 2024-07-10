using System;

namespace Spp.Common.EventSourcing;

public readonly struct TypeDefinition(string name, Type clrType)
{
    [Obsolete("Use constructor with parameters.")]
    public TypeDefinition() : this(null!, null!)
    {
    }

    public string Name { get; } = name;

    public Type ClrType { get; } = clrType;
}
