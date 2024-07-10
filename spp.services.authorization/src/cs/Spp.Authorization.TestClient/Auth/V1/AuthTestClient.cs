using Spp.Common.Miscellaneous;
using System;
using System.Net.Http;
using System.Text.Json;

namespace Spp.Authorization.TestClient.Auth.V1;

public partial class AuthTestClient
{
    public AuthTestClient(HttpClient httpClient)
        : this(httpClient, new JsonSerializerOptions(JsonSerializerDefaults.Web))
    {
    }

    partial void EnumToString<T>(T value, ref string? result) where T : struct, Enum
    {
        result = EnumSerializer.ToString(value);
    }
}
