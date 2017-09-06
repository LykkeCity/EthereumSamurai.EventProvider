namespace EthereumSamurai.EventProvider.Service.Actors.Extensions
{
    using Behaviors.Extensions;
    using Microsoft.Extensions.DependencyInjection;



    internal static class ActorsServiceCollectionExtensions
    {
        public static IServiceCollection AddActors(this IServiceCollection services)
        {
            services
                .AddBehaviors();

            services
                .AddTransient<Erc20BalanceChangesObserverActor>()
                .AddTransient<Erc20BalanceChangesReplayManagerActor>()
                .AddTransient<Erc20BalanceChangesSubscribtionManagerActor>();

            services
                .AddTransient<Erc20TransferCommitsObserverActor>()
                .AddTransient<Erc20TransferCommitsReplayManagerActor>()
                .AddTransient<Erc20TransferCommitsSubscriptionManagerActor>();

            services
                .AddTransient<IndexerNotificationsListenerActor>();

            services
                .AddTransient<SubscribersNotifierActor>();

            return services;
        }
    }
}