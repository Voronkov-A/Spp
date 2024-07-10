using System;
using Spp.Common.Miscellaneous;

namespace Spp.Authorization.Domain.Common.Exceptions;

public class InvalidNameException(string message, string name) : ApplicationException(message)
{
    public string Name { get; } = name;

    public override string ToString()
    {
        return ExceptionUtils.ToString(this, $"Name: {Name}");
    }
}
