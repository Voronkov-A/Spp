using System;
using System.Text;

namespace Spp.Common.Miscellaneous;

public static class ExceptionUtils
{
    public static string ToString(Exception exception, string details)
    {
        var sb = new StringBuilder(exception.GetType().ToString())
            .Append(": ")
            .Append(exception.Message)
            .AppendLine()
            .Append(details);

        if (exception.InnerException != null)
        {
            sb
                .Append(" ---> ")
                .AppendLine(exception.InnerException.ToString())
                .Append("   --- End of inner exception stack trace ---");
        }

        if (exception.StackTrace != null)
        {
            sb.AppendLine().Append(exception.StackTrace);
        }

        return sb.ToString();
    }
}
