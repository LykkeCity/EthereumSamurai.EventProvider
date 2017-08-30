namespace EthereumSamurai.EventProvider.Core.Configuration
{
    using System;
    using System.IO;
    using System.Net.Http;
    using Exceptions;
    using Microsoft.Extensions.Configuration;


    internal static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddLykkeSettings(this IConfigurationBuilder builder, string connectionStringName)
        {
            return builder.AddLykkeSettings(new HttpClientHandler(), connectionStringName);
        }

        internal static IConfigurationBuilder AddLykkeSettings(this IConfigurationBuilder builder, HttpMessageHandler handler, string connectionStringName)
        {
            if (string.IsNullOrEmpty(connectionStringName))
            {
                throw new ArgumentException
                (
                    message:   "Lykke settings connection string name should not be null or empty.",
                    paramName: nameof(connectionStringName)
                );
            }

            var configuration     = builder.Build();
            var settingsUriString = configuration.GetConnectionString(connectionStringName);
            
            if (Uri.TryCreate(settingsUriString, UriKind.Absolute, out var settingsUri))
            {
                try
                {
                    var httpClient   = new HttpClient(handler);
                    var settingsData = httpClient.GetStringAsync(settingsUri).Result;
                    var settingsFile = Path.GetTempFileName();

                    File.WriteAllText(settingsFile, settingsData);

                    builder.AddJsonFile(settingsFile);
                }
                catch (Exception e)
                {
                    throw new LykkeSettingsException("Failed to load Lykke settings.", e);
                }
            }
            else if (!string.IsNullOrEmpty(settingsUriString))
            {
                throw new LykkeSettingsException($"Lykke settings connection string [{settingsUriString}] is not valid url.");
            }

            return builder;
        }
    }
}