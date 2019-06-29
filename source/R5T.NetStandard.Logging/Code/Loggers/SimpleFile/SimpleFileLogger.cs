using System;

using Microsoft.Extensions.Logging;


namespace R5T.NetStandard.Logging.SimpleFile
{
    public class SimpleFileLogger : ILogger
    {
        private SimpleFileLoggerProvider Provider { get; }
        private string CategoryName { get; }
        private Func<LogLevel, bool> LogLevelFilter { get; }


        public SimpleFileLogger(SimpleFileLoggerProvider provider, string categoryName, Func<LogLevel, bool> logLevelFilter)
        {
            this.Provider = provider;
            this.CategoryName = categoryName;
            this.LogLevelFilter = logLevelFilter;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            var isEnabled = this.LogLevelFilter(logLevel);
            return isEnabled;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!this.IsEnabled(logLevel))
            {
                return;
            }

            var formattedStateAndException = formatter(state, exception);

            this.Provider.Log(logLevel, this.CategoryName, eventId, formattedStateAndException);
        }
    }
}
