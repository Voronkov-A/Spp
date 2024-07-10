using System;
using System.Net.Http;
using System.Text.Json;
using Spp.Common.Miscellaneous;

namespace Spp.IdentityProvider.TestClient.Applications.V1;

public partial class ApplicationsTestClient
{
    public ApplicationsTestClient(Uri baseAddress)
        : this(
            new HttpClient
            {
                BaseAddress = baseAddress
            })
    {
    }

    public ApplicationsTestClient(HttpClient httpClient)
        : this(httpClient, new JsonSerializerOptions(JsonSerializerDefaults.Web))
    {
    }

    partial void EnumToString<T>(T value, ref string? result) where T : struct, Enum
    {
        result = EnumSerializer.ToString(value);
    }
}
