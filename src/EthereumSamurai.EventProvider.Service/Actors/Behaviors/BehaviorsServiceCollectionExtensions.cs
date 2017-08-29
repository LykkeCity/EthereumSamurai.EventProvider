﻿namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using Microsoft.Extensions.DependencyInjection;

    internal static class BehaviorsServiceCollectionExtensions
    {
        public static IServiceCollection AddBehaviors(this IServiceCollection services)
        {
            services
                .AddTransient<IErc20BalanceChangesObserverBehavior, Erc20BalanceChangesObserverBehavior>()
                .AddTransient<IErc20BalanceChangesReplayManagerBehavior, Erc20BalanceChangesReplayManagerBehavior>()
                .AddTransient<IErc20BalanceChangesSubscriptionManagerBehavior, Erc20BalanceChangesSubscriptionManagerBehavior>();

            services
                .AddTransient<IErc20TransferCommitsObserverBehavior, Erc20TransferCommitsObserverBehavior>()
                .AddTransient<IErc20TransferCommitsReplayManagerBehavior, Erc20TransferCommitsReplayManagerBehavior>()
                .AddTransient<IErc20TransferCommitsSubscriptionManagerBehavior, Erc20TransferCommitsSubscriptionManagerBehavior>();

            services
                .AddTransient<IIndexerNotificationListenerBehavior, IndexerNotificationListenerBehavior>();

            services
                .AddTransient<ISubscriberNotifierBehavior, SubscriberNotifierBehavior>();

            return services;
        }
    }
}