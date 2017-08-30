﻿namespace EthereumSamurai.EventProvider.Service.Repositories.Factories
{
    using System;
    using Microsoft.Extensions.Caching.Memory;
    using MongoDB.Driver;
    using Options;
    using Subscriptions;


    public class Erc20SubscriptionRepositoryFactory : IErc20SubscriptionRepositoryFactory
    {
        private readonly IMemoryCache                       _cache;
        private readonly IMongoDatabase                     _database;
        private readonly Erc20SubscriptionRepositoryOptions _options;

        public Erc20SubscriptionRepositoryFactory(
            IMemoryCache                       cache,
            IMongoDatabase                     database,
            Erc20SubscriptionRepositoryOptions options)
        {
            _cache    = cache;
            _database = database;
            _options  = options;
        }


        public IErc20SubscriptionRepository<T> GetRepository<T>(string collectionName)
            where T : IErc20Subscription
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