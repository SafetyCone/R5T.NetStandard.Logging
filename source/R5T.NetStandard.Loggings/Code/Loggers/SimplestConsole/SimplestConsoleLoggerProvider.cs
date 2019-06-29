using System;

using Microsoft.Extensions.Logging;


namespace R5T.NetStandard.Logging.SimplestConsole
{
    /// <summary>
    /// Provides the very simplest of console loggers, one that accepts all log levels, and just writes messages to the console synchronously and with no extra formatting.
    /// </summary>
    public class SimplestConsoleLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new SimplestConsoleLogger();
        }

        public void Dispose()
        {
            // Do nothing, nothing to do.
        }
    }
}
