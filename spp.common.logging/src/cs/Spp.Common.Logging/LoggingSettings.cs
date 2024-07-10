namespace Spp.Common.Logging;

public class LoggingSettings
{
    public required LoggingProperties Properties { get; init; }

    public LoggerSettings? Console { get; init; }

    public LoggerSettings? Debug { get; init; }
}
