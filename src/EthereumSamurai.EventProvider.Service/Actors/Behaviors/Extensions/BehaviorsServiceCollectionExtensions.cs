namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors.Extensions
{
    using System;
    using Factories;
    using Factories.Interfaces;
    using Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;


    internal static class BehaviorsServiceCollectionExtensions
    {
        public static IServiceCollection AddBehaviors(this IServiceCollection services)
        {
            services
                .AddSingleton<IChannelFactory, ChannelFactory>();

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
    }
}