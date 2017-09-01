namespace EthereumSamurai.EventProvider.Service.Extensions
{
    using Actors.Options;
    using Hosting.Options;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Repositories.Options;



    internal static class OptionsConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services
                .Configure<Erc20SubscriptionRepositoryOptions>(configuration.GetSection("Service:Repositories:Erc20SubscriptionRepository"));

            services
                .Configure<ServiceHostOptions>(configuration.GetSection("Service:Host"));

            services
                .Configure<IndexerNotificationListenerOptions>(configuration.GetSection("Service:Actors:IndexerNotificationsListener"));

            return services;
        }
    }
}