namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using RabbitMQ.Client;


    internal static class BehaviorsServiceCollectionExtensions
    {
        public static IServiceCollection AddBehaviors(this IServiceCollection services)
        {
            services
                .AddRabbitMqChannel();

            services
                .AddTransient<IErc20BalanceChangesObserverBehavior, Erc20BalanceChangesObserverBehavior>()
                .AddTransient<IErc20BalanceChangesReplayManagerBehavior, Erc20BalanceChangesReplayManagerBehavior>()
                .AddTransient<IErc20BalanceChangesSubscriptionManagerBehavior, Erc20BalanceChangesSubscriptionManagerBehavior>();

            services
                .AddTransient<IErc20TransferCommitsObserverBehavior, Erc20TransferCommitsObserverBehavior>()
                .AddTransient<IErc20TransferCommitsReplayManagerBehavior, Erc20TransferCommitsReplayManagerBehavior>()
                .AddTransient<IErc20TransferCommitsSubscriptionManagerBehavior, Erc20TransferCommitsSubscriptionManagerBehavior>();

            services
                .AddTransient<IIndexerNotificationsListenerBehavior, IndexerNotificationsesListenerBehavior>();

            services
                .AddTransient<ISubscriberNotifierBehavior, SubscriberNotifierBehavior>();

            return services;
        }

        private static IServiceCollection AddRabbitMqChannel(this IServiceCollection services)
        {
            return services.AddSingleton(provider =>
            {
                var configuration    = provider.GetService<IConfigurationRoot>();
                var connectionString = configuration.GetConnectionString("RabbitMQ");
                var rabbitUri        = new Uri(connectionString);

                var connectionFactory = new ConnectionFactory
                {
                    AutomaticRecoveryEnabled = true,
                    Uri                      = rabbitUri
                };

                var connection = connectionFactory.CreateConnection();
                
                return connection.CreateModel();
            });
        }
    }
}