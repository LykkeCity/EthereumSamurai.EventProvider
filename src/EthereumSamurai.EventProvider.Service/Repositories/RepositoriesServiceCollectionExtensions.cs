namespace EthereumSamurai.EventProvider.Service.Repositories
{
    using Entities;
    using Entities.Interfaces;
    using Factories;
    using Factories.Interfaces;
    using Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MongoDB.Driver;


    internal static class RepositoriesServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddMongoDb()
                .AddErc20SubscriptionRepositories()
                .AddSingleton<IErc20BalanceRepository, Erc20BalanceRepository>()
                .AddSingleton<IErc20TransferHistoryRepository, Erc20TransferHistoryRepository>();
            
            return services;
        }

        private static IServiceCollection AddErc20SubscriptionRepository<T>(this IServiceCollection services, string collectionName)
            where T : IErc20SubscriptionEntity, new()
        {
            return services.AddSingleton(provider =>
            {
                var factory = provider.GetRequiredService<IErc20SubscriptionRepositoryFactory>();
                
                return factory.GetRepository<T>(collectionName);
            });
        }

        private static IServiceCollection AddErc20SubscriptionRepositories(this IServiceCollection services)
        {
            return services
                .AddSingleton<IErc20SubscriptionRepositoryFactory, Erc20SubscriptionRepositoryFactory>()
                .AddErc20SubscriptionRepository<Erc20BalanceChangesSubscriptionEntity>("Erc20BalanceChanges")
                .AddErc20SubscriptionRepository<Erc20TransferCommitsSubscriptionEntity>("Erc20TransferCommits");
        }

        private static IServiceCollection AddMongoDb(this IServiceCollection services)
        {
            return services.AddSingleton(provider =>
            {
                var configuration    = provider.GetService<IConfigurationRoot>();
                var connectionString = configuration.GetConnectionString("MongoDb");

                var mongoUrl    = MongoUrl.Create(connectionString);
                var mongoClient = new MongoClient(mongoUrl);
                var mongoDb     = mongoClient.GetDatabase(mongoUrl.DatabaseName);

                return mongoDb;
            });
        }
    }
}