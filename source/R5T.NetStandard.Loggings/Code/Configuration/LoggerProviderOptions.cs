using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging.Configuration;


namespace R5T.NetStandard.Logging.Configuration
{
    /// <summary>
    /// Provides a set of helpers to initialize options objects from logger provider configuration.
    /// </summary>
    /// <remarks>
    /// Copied from: https://github.com/aspnet/Logging/blob/master/src/Microsoft.Extensions.Logging.Configuration/LoggerProviderConfigurationExtensions.cs
    /// This class SHOULD have been exposed by the DLL but seemingly (at least in 2.1) was not.
    /// </remarks>
    public static class LoggerProviderOptions
    {
        /// <summary>
        /// Indicates that settings for <typeparamref name="TProvider"/> should be loaded into <typeparamref name="TOptions"/> type.
        /// </summary>
        /// <typeparam name="TOptions">The options class </typeparam>
        /// <typeparam name="TProvider">The provider class</typeparam>
        public static void RegisterProviderOptions<TOptions, TProvider>(IServiceCollection services) where TOptions : class
        {
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<TOptions>, LoggerProviderConfigureOptions<TOptions, TProvider>>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IOptionsChangeTokenSource<TOptions>, LoggerProviderOptionsChangeTokenSource<TOptions, TProvider>>());
        }
    }
}
