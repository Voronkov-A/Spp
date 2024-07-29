using System;
using System.Net.Http;
using Spp.Common.Miscellaneous;
using Spp.Common.Miscellaneous.Serialization;

namespace Spp.Authorization.TestClient.Users.V1;

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
        : this(httpClient, DefaultJsonSerializer.Options)
    {
    }

    partial void EnumToString<T>(T value, ref string? result) where T : struct, Enum
    {
        result = EnumSerializer.ToString(value);
    }
}
