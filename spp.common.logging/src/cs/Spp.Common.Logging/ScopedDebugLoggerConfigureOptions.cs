using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace Spp.Common.Logging;

public class ScopedDebugLoggerConfigureOptions(
    ILoggerProviderConfiguration<ScopedDebugLoggerProvider> providerConfiguration) :
    IConfigureOptions<ScopedDebugLoggerOptions>
{
    private readonly IConfiguration _configuration = providerConfiguration.Configuration;

    public void Configure(ScopedDebugLoggerOptions options)
    {
        _configuration.Bind(options);
    }
}
