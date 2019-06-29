using System;

using Microsoft.Extensions.Logging;


namespace R5T.NetStandard.Logging.SimplestFile
{
    public class SimplestFileLogger : ILogger
    {
        private SimplestFileLoggerProvider Provider { get; }
        private string CategoryName { get; }


        public SimplestFileLogger(SimplestFileLoggerProvider provider, string categoryName)
        {
            this.Provider = provider;
            this.CategoryName = categoryName;
        }

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
            var formattedStateAndException = formatter(state, exception);

            this.Provider.Log(logLevel, this.CategoryName, eventId, formattedStateAndException);
        }
    }
}
