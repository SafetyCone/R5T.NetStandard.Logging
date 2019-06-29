using System;

using Microsoft.Extensions.Logging;


namespace R5T.NetStandard.Logging.Logger
{
    /// <summary>
    /// Allows logging to a <see cref="ILogger"/>.
    /// </summary>
    public class LoggerLogger : ILogger
    {
        private ILogger Logger { get; }


        public LoggerLogger(ILogger logger)
        {
            this.Logger = logger;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            this.Logger.Log(logLevel, eventId, state, exception, formatter);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            var output = this.Logger.IsEnabled(logLevel);
            return output;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            var output = this.Logger.BeginScope<TState>(state);
            return output;
        }
    }
}
