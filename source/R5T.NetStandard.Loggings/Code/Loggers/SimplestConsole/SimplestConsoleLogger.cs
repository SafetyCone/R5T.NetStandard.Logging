using System;

using Microsoft.Extensions.Logging;


namespace R5T.NetStandard.Logging.SimplestConsole
{
    /// <summary>
    /// The very simplest logger. Accepts all log levels and just writes messages to the console synchronously and with no extra formatting.
    /// </summary>
    public class SimplestConsoleLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var logMessage = formatter(state, exception);

            Console.WriteLine(logMessage);
        }
    }
}
