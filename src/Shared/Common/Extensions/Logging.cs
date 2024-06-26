﻿using Microsoft.Extensions.Logging;
using Serilog;

namespace Common.Extensions;

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

