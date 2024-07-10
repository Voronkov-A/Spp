using System;
using System.Net.Http;
using System.Text.Json;
using Spp.Common.Miscellaneous;

namespace Spp.IdentityProvider.TestClient.Users.V1;

public partial class UsersTestClient
{
    public UsersTestClient(Uri baseAddress)
        : this(
            new HttpClient
            {
                BaseAddress = baseAddress
            })
    {
    }

    public UsersTestClient(HttpClient httpClient)
        : this(httpClient, new JsonSerializerOptions(JsonSerializerDefaults.Web))
    {
    }

    partial void EnumToString<T>(T value, ref string? result) where T : struct, Enum
    {
        result = EnumSerializer.ToString(value);
    }
}
