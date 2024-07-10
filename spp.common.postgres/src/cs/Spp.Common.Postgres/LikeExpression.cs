namespace Spp.Common.Postgres;

public static class LikeExpression
{
    public static string Escape(string value)
    {
        return value.Replace("%", "\\%").Replace("_", "\\_");
    }
}
