namespace EthereumSamurai.EventProvider.Service.Repositories
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    internal static class RepositoriesServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddSingleton<IErc20BalanceRepository, Erc20BalanceRepository>();

            services
                .AddSingleton(BuildSubscriptionsRepository<IErc20BalanceChanges>);

            services
                .AddSingleton(BuildSubscriptionsRepository<IERc20TransferCommits>);

            services
                .AddSingleton<IErc20TransferHistoryRepository, Erc20TransferHistoryRepository>();

            return services;
        }

        private static IErc20SubscriptionRepository<T> BuildSubscriptionsRepository<T>(IServiceProvider provider)
            where T : IErc20SubscriptionType
        {

            throw new NotImplementedException();
        }
    }
}