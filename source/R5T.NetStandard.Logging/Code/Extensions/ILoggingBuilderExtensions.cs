using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

using R5T.NetStandard.Logging.Configuration;
using R5T.NetStandard.Logging.SimpleConsole;
using R5T.NetStandard.Logging.SimpleFile;
using R5T.NetStandard.Logging.SimplestConsole;
using R5T.NetStandard.Logging.SimplestFile;



namespace R5T.NetStandard.Logging
{
    public static class ILoggingBuilderExtensions
    {
        public static ILoggingBuilder AddSimpleConsole(this ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddSimpleConsole(DummyLogger.Instance);

            return loggingBuilder;
        }

        public static ILoggingBuilder AddSimpleConsole(this ILoggingBuilder loggingBuilder, ILogger logger)
        {
            logger.LogDebug($@"Adding {nameof(SimpleConsoleLogger)}...");

            loggingBuilder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, SimpleConsoleLoggerProvider>());

            logger.LogInformation($@"Added {nameof(SimpleConsoleLogger)}.");

            return loggingBuilder;
        }

        public static ILoggingBuilder AddSimpleFile(this ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddSimpleFile(DummyLogger.Instance);

            return loggingBuilder;
        }

        public static ILoggingBuilder AddSimpleFile(this ILoggingBuilder loggingBuilder, ILogger logger)
        {
            logger.LogDebug($@"Adding {nameof(SimpleFileLogger)}...");

            loggingBuilder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, SimpleFileLoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions<SimpleFileLoggerOptions, SimpleFileLoggerProvider>(loggingBuilder.Services);

            logger.LogInformation($@"Added {nameof(SimpleFileLogger)}.");

            return loggingBuilder;
        }

        public static ILoggingBuilder AddSimplestConsole(this ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddSimplestConsole(DummyLogger.Instance);

            return loggingBuilder;
        }

        public static ILoggingBuilder AddSimplestConsole(this ILoggingBuilder loggingBuilder, ILogger logger)
        {
            logger.LogDebug($@"Adding {nameof(SimplestConsoleLogger)}...");

            loggingBuilder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, SimplestConsoleLoggerProvider>());

            logger.LogInformation($@"Added {nameof(SimplestConsoleLogger)}.");

            return loggingBuilder;
        }

        public static ILoggingBuilder AddSimplestFile(this ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddSimplestFile(DummyLogger.Instance);

            return loggingBuilder;
        }

        public static ILoggingBuilder AddSimplestFile(this ILoggingBuilder loggingBuilder, ILogger logger)
        {
            logger.LogDebug($@"Adding {nameof(SimplestFileLogger)}...");

            loggingBuilder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, SimplestFileLoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions<SimplestFileLoggerOptions, SimplestFileLoggerProvider>(loggingBuilder.Services);

            logger.LogInformation($@"Added {nameof(SimplestFileLogger)}.");

            return loggingBuilder;
        }
    }
}
