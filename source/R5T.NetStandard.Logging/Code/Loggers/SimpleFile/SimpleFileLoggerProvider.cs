using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using R5T.NetStandard.IO;

using PathUtilities = R5T.NetStandard.IO.Paths.Utilities;
using PathUtilitiesExtra = R5T.NetStandard.IO.Paths.UtilitiesExtra;


namespace R5T.NetStandard.Logging.SimpleFile
{
    [ProviderAlias(SimpleFileLoggerProvider.ProviderAliasName)]
    public class SimpleFileLoggerProvider : ILoggerProvider
    {
        public const string ProviderAliasName = @"SimpleFile";


        private object LockObject { get; } = new object();
        private Dictionary<string, LogLevel> LogLevelsByCategoryName { get; } = new Dictionary<string, LogLevel>();
        private string LogFilePath { get; set; }
        private bool Overwrite { get; set; }


        public SimpleFileLoggerProvider(IOptions<SimpleFileLoggerOptions> simplestFileLoggerOptionsOptions, IOptions<LoggerFilterOptions> loggerFilterOptionsOptions)
        {
            var rawLogFilePath = simplestFileLoggerOptionsOptions.Value.LogFilePath;

            this.LogFilePath = PathUtilitiesExtra.EnsureDirectorySeparator(rawLogFilePath);

            var overwrite = simplestFileLoggerOptionsOptions.Value.Overwrite;
            if (overwrite)
            {
                if (File.Exists(this.LogFilePath))
                {
                    File.Delete(this.LogFilePath);
                }
            }

            var loggerFilterOptions = loggerFilterOptionsOptions.Value;

            // Get the logger filter rules for the specific provider.
            var filterRules = loggerFilterOptions.Rules.Where(x => x.ProviderName == SimpleFileLoggerProvider.ProviderAliasName).ToList();
            if (filterRules.Count < 1)
            {
                // Use the general rules instead.
                filterRules = loggerFilterOptions.Rules.Where(x => x.ProviderName == null).ToList();
            }

            foreach (var filterRule in filterRules)
            {
                this.LogLevelsByCategoryName.Add(filterRule.CategoryName ?? Utilities.DefaultCategoryName, filterRule.LogLevel ?? LogLevel.None);
            }
        }

        public ILogger CreateLogger(string categoryName)
        {
            var logLevelFilter = Utilities.GetLogLevelFilter(categoryName, this.LogLevelsByCategoryName);

            return new SimpleFileLogger(this, categoryName, logLevelFilter);
        }

        public void Dispose()
        {
            // Do nothing, nothing to do.
        }

        internal void Log(LogLevel logLevel, string categoryName, EventId eventId, string formattedStateAndException)
        {
            lock (this.LockObject)
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var writer = StreamWriterHelper.NewLeaveOpen(memoryStream))
                    {
                        this.PerformLog(writer, logLevel, categoryName, eventId, formattedStateAndException);
                    }

                    using (var fileStream = new FileStream(this.LogFilePath, FileMode.OpenOrCreate))
                    {
                        fileStream.Seek(0, SeekOrigin.End);

                        memoryStream.Seek(0, SeekOrigin.Begin);
                        memoryStream.CopyTo(fileStream);
                    }
                }
            }
        }

        private void PerformLog(StreamWriter writer, LogLevel logLevel, string categoryName, EventId eventId, string formattedStateAndException)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    writer.Write(@"trc ");
                    break;

                case LogLevel.Debug:
                    writer.Write(@"dbg ");
                    break;

                case LogLevel.Information:
                    writer.Write(@"inf ");
                    break;

                case LogLevel.Warning:
                    writer.WriteLine(@"WARNING");
                    writer.Write(Utilities.DefaultLinePrefix);
                    break;

                case LogLevel.Error:
                    writer.WriteLine(@"* ERROR * ");
                    writer.Write(Utilities.DefaultLinePrefix);
                    break;

                case LogLevel.Critical:
                    writer.WriteLine(@"*** CRITICAL *** ");
                    writer.Write(Utilities.DefaultLinePrefix);
                    break;

                default:
                    writer.Write(Utilities.DefaultLinePrefix);
                    break;
            }

            writer.WriteLine(categoryName);

            Utilities.WriteLines(writer, formattedStateAndException, Utilities.DefaultLinePrefix);
        }
    }
}
