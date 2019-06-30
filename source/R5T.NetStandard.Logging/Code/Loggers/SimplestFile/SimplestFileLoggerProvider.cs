using System;
using System.IO;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using R5T.NetStandard.IO;

using PathUtilities = R5T.NetStandard.IO.Paths.UtilitiesExtra;
using PathUtilitiesExtra = R5T.NetStandard.IO.Paths.UtilitiesExtra;


namespace R5T.NetStandard.Logging.SimplestFile
{
    [ProviderAlias(SimplestFileLoggerProvider.ProviderAliasName)]
    public class SimplestFileLoggerProvider : ILoggerProvider
    {
        public const string ProviderAliasName = @"SimplestFile";



        private object LockObject { get; } = new object();
        private string LogFilePath { get; set; }
        private bool Overwrite { get; set; }


        public SimplestFileLoggerProvider(string logFilePath, bool overwrite = true)
        {
            this.LogFilePath = logFilePath;
            this.Overwrite = overwrite;
        }

        public SimplestFileLoggerProvider(IOptions<SimplestFileLoggerOptions> simplestFileLoggerOptionsOptions)
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
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new SimplestFileLogger(this, categoryName);
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
