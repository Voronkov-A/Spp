using Spp.Common.Miscellaneous;
using Spp.Common.Miscellaneous.Serialization;
using System;
using System.Net.Http;

namespace Spp.Authorization.TestClient.Callback.V1;

public partial class CallbackTestClient
{
    public CallbackTestClient(HttpClient httpClient)
        : this(httpClient, DefaultJsonSerializer.Options)
    {
    }

    partial void EnumToString<T>(T value, ref string? result) where T : struct, Enum
    {
        result = EnumSerializer.ToString(value);
    }
}
