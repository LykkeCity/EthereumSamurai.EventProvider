namespace EthereumSamurai.EventProvider.Core.Repositories.Factories
{
    using System;
    using Microsoft.Extensions.Caching.Memory;
    using MongoDB.Driver;

    public class Erc20SubscriptionRepositoryFactory : IErc20SubscriptionRepositoryFactory
    {
        private readonly IMemoryCache   _cache;
        private readonly IMongoDatabase _database;


        public Erc20SubscriptionRepositoryFactory(IMongoDatabase database, IMemoryCache cache)
        {
            _cache    = cache;
            _database = database;
        }


        public IErc20SubscriptionRepository GetRepository(string collectionName)
        {
            return new Erc20SubscriptionRepository
            (
                database:       _database,
                collectionName: collectionName
            );
        }

        public IErc20SubscriptionRepository GetRepositoryWithCache(string collectionName, TimeSpan cacheDuration)
        {
            return new Erc20SubscriptionRepositoryWithCache
            (
                cache:         _cache,
                cacheDuration: cacheDuration,
                repository:    GetRepository(collectionName)
            );
        }
    }
}