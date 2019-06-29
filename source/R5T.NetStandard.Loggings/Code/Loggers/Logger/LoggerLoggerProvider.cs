using System;

using Microsoft.Extensions.Logging;


namespace R5T.NetStandard.Logging.Logger
{
    /// <summary>
    /// Allows logging to an already existing <see cref="ILogger"/> instance.
    /// </summary>
    public class LoggerLoggerProvider : ILoggerProvider
    {
        private ILogger Logger { get; }


        public LoggerLoggerProvider(ILogger logger)
        {
            this.Logger = logger;
        }

        public ILogger CreateLogger(string categoryName)
        {
            var logger = new LoggerLogger(this.Logger);
            return logger;
        }

        public void Dispose()
        {
            // Do nothing, nothing to do.
        }
    }
}
