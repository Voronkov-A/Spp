using Spp.Common.Miscellaneous;
using System;
using System.Net.Http;
using System.Text.Json;

namespace Spp.Authorization.TestClient.Callback.V1;

public partial class CallbackTestClient
{
    public CallbackTestClient(HttpClient httpClient)
        : this(httpClient, new JsonSerializerOptions(JsonSerializerDefaults.Web))
    {
    }

    partial void EnumToString<T>(T value, ref string? result) where T : struct, Enum
    {
        result = EnumSerializer.ToString(value);
    }
}
