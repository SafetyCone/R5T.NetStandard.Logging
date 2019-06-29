using System;

using Microsoft.Extensions.Logging;


namespace R5T.NetStandard.Logging
{
    public static class Constants
    {
        public const LogLevel DefaultMinimumLogLevel = LogLevel.Information;

        public const string ConfigurationLoggingSectionKey = @"Logging";
        public const string ConfigurationLoggingSectionPath = Constants.ConfigurationLoggingSectionKey;
        public const string LogFilePathKeyName = @"LogFilePath";
    }
}
