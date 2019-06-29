using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;


namespace R5T.NetStandard.Logging.SimpleConsole
{
    [ProviderAlias(SimpleConsoleLoggerProvider.ProviderAliasName)]
    public class SimpleConsoleLoggerProvider : ILoggerProvider
    {
        public const string ProviderAliasName = @"SimpleConsole";


        private Dictionary<string, LogLevel> LogLevelsByCategoryName { get; } = new Dictionary<string, LogLevel>();


        public SimpleConsoleLoggerProvider(ILoggerProviderConfiguration<SimpleConsoleLoggerProvider> loggerProviderConfiguration, IOptions<LoggerFilterOptions> loggerFilterOptionsOptions)
        {
            var loggerFilterOptions = loggerFilterOptionsOptions.Value;

            // Get the logger filter rules for the specific provider.
            var filterRules = loggerFilterOptions.Rules.Where(x => x.ProviderName == SimpleConsoleLoggerProvider.ProviderAliasName).ToList();
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

            var logger = new SimpleConsoleLogger(categoryName, logLevelFilter);
            return logger;
        }

        public void Dispose()
        {
            // Do nothing, nothing to do.
        }
    }
}
