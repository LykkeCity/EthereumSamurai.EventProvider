namespace EthereumSamurai.EventProvider.Core.Configuration
{
    using System;
    using System.IO;
    using System.Net.Http;
    using Microsoft.Extensions.Configuration;

    internal static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddLykkeSettings(this IConfigurationBuilder builder, string connectionStringName)
        {
            return builder.AddLykkeSettings(new HttpClientHandler(), connectionStringName);
        }

        internal static IConfigurationBuilder AddLykkeSettings(this IConfigurationBuilder builder, HttpMessageHandler handler, string connectionStringName)
        {
            var configuration     = builder.Build();
            var settingsUriString = configuration.GetConnectionString(connectionStringName);
            
            if (Uri.TryCreate(settingsUriString, UriKind.Absolute, out var settingsUri))
            {
                var httpClient   = new HttpClient(handler);
                var settingsData = httpClient.GetStringAsync(settingsUri).Result;
                var settingsFile = Path.GetTempFileName();
                
                File.WriteAllText(settingsFile, settingsData);

                builder.AddJsonFile(settingsFile);
            }

            return builder;
        }
    }
}