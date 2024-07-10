namespace Spp.Common.Miscellaneous;

public static class StringExtensions
{
    public static string EnsureEndsWith(this string value, char endingSubstring)
    {
        return value.EndsWith(endingSubstring) ? value : value + endingSubstring;
    }
}
