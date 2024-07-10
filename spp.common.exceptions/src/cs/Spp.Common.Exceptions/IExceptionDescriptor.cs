using System;

namespace Spp.Common.Exceptions;

public interface IExceptionDescriptor
{
    bool IsTransient(Exception exception);
}
