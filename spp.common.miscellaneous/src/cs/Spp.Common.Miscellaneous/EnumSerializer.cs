using FastEnumUtility;
using System;
using System.Linq;

namespace Spp.Common.Miscellaneous;

public static class EnumSerializer
{
    public static string ToString<T>(T value) where T : struct, Enum
    {
        return FastEnum.GetMember(value)?.EnumMemberAttribute?.Value ?? value.ToString();
    }

    public static T FromString<T>(string value) where T : struct, Enum
    {
        return FastEnum.GetMembers<T>().FirstOrDefault(it => it.EnumMemberAttribute?.Value == value)?.Value
            ?? FastEnum.Parse<T>(value);
    }
}
