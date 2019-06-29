using System;

using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging.Configuration;


namespace R5T.NetStandard.Logging.Configuration
{
    /// <summary>
    /// Loads settings for <typeparamref name="TProvider"/> into <typeparamref name="TOptions"/> type.
    /// </summary>
    /// <remarks>
    /// Copied from: https://github.com/aspnet/Logging/blob/master/src/Microsoft.Extensions.Logging.Configuration/LoggerProviderConfigureOptions.cs
    /// This was required for the the <see cref="LoggerProviderOptions"/> class, which SHOULD have been exposed by the DLL but seemingly (at least in 2.1) is not.
    /// </remarks>
    internal class LoggerProviderConfigureOptions<TOptions, TProvider> : ConfigureFromConfigurationOptions<TOptions> where TOptions : class
    {
        public LoggerProviderConfigureOptions(ILoggerProviderConfiguration<TProvider> providerConfiguration)
            : base(providerConfiguration.Configuration)
        {
        }
    }
}
