using System;

using Microsoft.Extensions.Logging;


namespace R5T.NetStandard.Logging
{
    /// <summary>
    /// Adapted from: https://app.pluralsight.com/player?course=entity-framework-core-getting-started&amp;author=julie-lerman&amp;name=entity-framework-core-getting-started-m4&amp;clip=2&amp;mode=live
    /// </summary>
    public class ExtremelySimpleLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new ExtremelySimpleLogger();
        }

        public void Dispose()
        {
            // Do nothing, nothing to do.
        }


        private class ExtremelySimpleLogger : ILogger
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
                Console.WriteLine(formatter(state, exception));
            }
        }
    }
}
