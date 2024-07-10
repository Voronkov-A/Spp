using System;

namespace Spp.Common.Exceptions;

public interface IExceptionResolver
{
    bool IsTransient(Exception exception);
}
