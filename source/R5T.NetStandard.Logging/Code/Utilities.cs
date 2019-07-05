using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using R5T.NetStandard.IO;

using BasicConstants = R5T.NetStandard.Logging.Types.Constants;


namespace R5T.NetStandard.Logging
{
    /// <summary>
    /// Utilities for use with common NetStandard logging functionality.
    /// </summary>
    public static class Utilities
    {
        public const char LoggerCategoryTokenSeparatorChar = '.';
        public static readonly string LoggerCategoryTokenSeparator = Utilities.LoggerCategoryTokenSeparatorChar.ToString();
        public const string DefaultCategoryName = @"Default";
        /// <summary>
        /// The default log level to use when a log level is required but cannot be provied. Different from <see cref="BasicConstants.DefaultMinimumLogLevel"/>, which is the default minimum log level.
        /// </summary>
        public const LogLevel DefaultLogLevel = LogLevel.Information;
        public const string DefaultLinePrefix = @"    "; // 4 spaces.


        /// <summary>
        /// Gets the configuration key for the log file path value.
        /// </summary>
        public static string GetLogFilePathConfigurationKey()
        {
            var output = ConfigurationPath.Combine(Constants.ConfigurationLoggingSectionPath, Constants.LogFilePathKeyName);
            return output;
        }

        public static IEnumerable<string> GetCategoryNamePrefixes(string categoryName)
        {
            while (!string.IsNullOrEmpty(categoryName))
            {
                yield return categoryName;

                var lastIndexOfTokenSeparator = categoryName.LastIndexOf(Utilities.LoggerCategoryTokenSeparator);
                if (lastIndexOfTokenSeparator == -1)
                {
                    yield return Utilities.DefaultCategoryName;
                    break;
                }

                categoryName = categoryName.Substring(0, lastIndexOfTokenSeparator);
            }
        }

        public static Func<LogLevel, bool> GetLogLevelFilter(string categoryName, IDictionary<string, LogLevel> logLevelsByCategoryName, LogLevel defaultLogLevel = Utilities.DefaultLogLevel)
        {
            foreach (var categoryNamePrefix in Utilities.GetCategoryNamePrefixes(categoryName))
            {
                if (logLevelsByCategoryName.TryGetValue(categoryNamePrefix, out var logLevel))
                {
                    return l => l >= logLevel;
                }
            }

            return l => l >= defaultLogLevel;
        }

        public static void WriteLines(TextWriter writer, string formattedStateAndException, string linePrefix)
        {
            using (var stringReader = new StringReader(formattedStateAndException))
            {
                while (!stringReader.ReadLineIsEnd(out string line))
                {
                    writer.Write(linePrefix);
                    writer.WriteLine(line);
                }
            }
        }
    }
}
