using System;

namespace Spp.Common.Errors;

public interface IErrorUriProvider
{
    Uri GetTypeUri<TErrorCode>(TErrorCode code) where TErrorCode : struct, Enum;
}
