using System;
using System.Net.Http;
using System.Text.Json;
using Spp.Common.Miscellaneous;

namespace Spp.Authorization.TestClient.Rbac.V1;

public partial class RbacTestClient
{
    public RbacTestClient(Uri baseAddress)
        : this(
            new HttpClient
            {
                BaseAddress = baseAddress
            })
    {
    }

    public RbacTestClient(HttpClient httpClient)
        : this(httpClient, new JsonSerializerOptions(JsonSerializerDefaults.Web))
    {
    }

    partial void EnumToString<T>(T value, ref string? result) where T : struct, Enum
    {
        result = EnumSerializer.ToString(value);
    }
}
