using System;

using Microsoft.Extensions.Logging;


namespace R5T.NetStandard.Logging
{
    /// <summary>
    /// Adapted from: https://app.pluralsight.com/player?course=entity-framework-core-getting-started&amp;author=julie-lerman&amp;name=entity-framework-core-getting-started-m4&amp;clip=2&amp;mode=live
    /// </summary>
    public class SimpleLoggerProvider : ILoggerProvider
    {
        public static LogLevel LogLevel { get; set; } = LogLevel.Information;


        public ILogger CreateLogger(string categoryName)
        {
            return new SimpleLogger();
        }

        public void Dispose()
        {
            // Do nothing, nothing to do.
        }


        private class SimpleLogger : ILogger
        {
            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                var output = logLevel >= SimpleLoggerProvider.LogLevel;
                return output;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                Console.WriteLine(formatter(state, exception));
            }
        }
    }
}
