namespace EthereumSamurai.EventProvider.Service.Repositories.Factories
{
    using System;
    using Entities.Interfaces;
    using Interfaces;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using Options;
    using Repositories.Interfaces;



    public class Erc20SubscriptionRepositoryFactory : IErc20SubscriptionRepositoryFactory
    {
        private readonly IMemoryCache                       _cache;
        private readonly IMongoDatabase                     _database;
        private readonly Erc20SubscriptionRepositoryOptions _options;

        public Erc20SubscriptionRepositoryFactory(
            IMemoryCache cache,
            IMongoDatabase database,
            IOptions<Erc20SubscriptionRepositoryOptions> options)
        {
            _cache    = cache;
            _database = database;
            _options  = options.Value;
        }


        public IErc20SubscriptionRepository<T> GetRepository<T>(string collectionName)
            where T : IErc20SubscriptionEntity, new()
        {
            if (_options.UseCache)
            {
                return new Erc20SubscriptionRepositoryWithCache<T>
                (
                    cache:         _cache,
                    cacheDuration: TimeSpan.FromSeconds(_options.CacheDuration),
                    repository:    GetRepository<T>(collectionName)
                );
            }
            else
            {
                return new Erc20SubscriptionRepository<T>
                (
                    collectionName: collectionName,
                    database:       _database
                );
            }
        }
    }
}