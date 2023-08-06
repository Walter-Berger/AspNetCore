using Microsoft.Extensions.Logging;
using Serilog;

namespace Libraries.Extensions;

public static class Logging
{
    public static ILoggingBuilder AddCustomLogging(this ILoggingBuilder logging)
    {
        var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        logging.ClearProviders();
        logging.AddSerilog(logger);

        return logging;
    }
}

