namespace EthereumSamurai.EventProvider.Service.Options
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;


    internal static class OptionsConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services
                .Configure<ServiceHostOptions>(configuration.GetSection("Service:Host"));

            return services;
        }
    }
}