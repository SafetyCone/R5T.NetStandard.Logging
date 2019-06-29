using System;

using Microsoft.Extensions.Logging;


namespace R5T.NetStandard.Logging.SimpleConsole
{
    public class SimpleConsoleLogger : ILogger
    {
        public const string LinePrefix = @"      "; // 6 spaces.


        #region Static

        public static void PerformLogging(LogLevel logLevel, string categoryName, EventId eventId, string formattedStateAndException)
        {
            var initialForegroundColor = Console.ForegroundColor;
            var initialBackgroundColor = Console.BackgroundColor;

            switch (logLevel)
            {
                case LogLevel.Trace:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;

                case LogLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;

                case LogLevel.Information:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case LogLevel.Critical:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            switch (logLevel)
            {
                case LogLevel.Trace:
                    Console.Write(@"trace ");
                    break;

                case LogLevel.Debug:
                    Console.Write(@"debug ");
                    break;

                case LogLevel.Information:
                    Console.Write(@"info  ");
                    break;

                case LogLevel.Warning:
                    Console.WriteLine(@"WARNING");
                    Console.Write(SimpleConsoleLogger.LinePrefix); 
                    break;

                case LogLevel.Error:
                    Console.WriteLine(@"* ERROR * ");
                    Console.Write(SimpleConsoleLogger.LinePrefix);
                    break;

                case LogLevel.Critical:
                    Console.WriteLine(@"*** CRITICAL *** ");
                    Console.BackgroundColor = initialBackgroundColor;
                    Console.Write(SimpleConsoleLogger.LinePrefix);
                    break;

                default:
                    Console.Write(SimpleConsoleLogger.LinePrefix); // 6 spaces.
                    break;
            }

            Console.ForegroundColor = initialForegroundColor;
            Console.BackgroundColor = initialBackgroundColor;

            Console.WriteLine(categoryName);

            Utilities.WriteLines(Console.Out, formattedStateAndException, SimpleConsoleLogger.LinePrefix);
        }

        #endregion


        private string CategoryName { get; }
        private Func<LogLevel, bool> LogLevelFilter { get; }


        public SimpleConsoleLogger(string categoryName, Func<LogLevel, bool> logLevelFilter)
        {
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

            using (var consoleSynchronizationContext = ConsoleSynchronization.GetContext())
            {
                SimpleConsoleLogger.PerformLogging(logLevel, this.CategoryName, eventId, formattedStateAndException);
            }
        }
    }
}
