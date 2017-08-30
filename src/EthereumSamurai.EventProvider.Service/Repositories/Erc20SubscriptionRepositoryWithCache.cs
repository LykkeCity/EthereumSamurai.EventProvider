namespace EthereumSamurai.EventProvider.Service.Repositories
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;
    using Subscriptions;


    public class Erc20SubscriptionRepositoryWithCache<T> : IErc20SubscriptionRepository<T>
        where T : IErc20Subscription
    {
        private readonly IMemoryCache                    _cache;
        private readonly TimeSpan                        _cacheDuration;
        private readonly IErc20SubscriptionRepository<T> _repository;

        public Erc20SubscriptionRepositoryWithCache(
            IMemoryCache                    cache,
            TimeSpan                        cacheDuration,
            IErc20SubscriptionRepository<T> repository)
        {
            _cache         = cache;
            _cacheDuration = cacheDuration;
            _repository    = repository;
        }


        public string CollectionName
            => _repository.CollectionName;



        public IEnumerable<(string exchange, string routingKey)> GetSubscribers()
        {
            return _cache.GetOrCreate
            (
                GetCacheKeyForAllSubscribers(),
                x => ConfigureCacheEntry(x, _repository.GetSubscribers())
            );
        }

        public IEnumerable<(string exchange, string routingKey)> GetSubscribers(string assetHolder, string contract)
        {
            return _cache.GetOrCreate
            (
                GetCacheKeyForSubscription(assetHolder, contract),
                x => ConfigureCacheEntry(x, _repository.GetSubscribers(assetHolder, contract))
            );
        }

        public IEnumerable<(string exchange, string routingKey)> GetSubscribers(string assetHolder, string[] contracts)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<(string exchange, string routingKey)> GetSubscribers(string[] assetHolders, string contract)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(string exchange, string routingKey, string assetHolder, string contract)
        {
            _repository.Subscribe(exchange, routingKey, assetHolder, contract);

            InvalidateCache(assetHolder, contract);
        }

        public void Unsubscribe(string exchange, string routingKey, string assetHolder, string contract)
        {
            _repository.Unsubscribe(exchange, routingKey, assetHolder, contract);

            InvalidateCache(assetHolder, contract);
        }
        
        private T ConfigureCacheEntry<T>(ICacheEntry entry, T value)
        {
            entry.AbsoluteExpirationRelativeToNow = _cacheDuration;
            entry.Value = value;

            return value;
        }

        private string GetCacheKeyForAllSubscribers()
            => $"{CollectionName}/Subscribers";

        private string GetCacheKeyForSubscription(string assetHolder, string contract)
            => $"{CollectionName}/Subscriptions/{assetHolder}/{contract}";

        private void InvalidateCache(string assetHolder, string contract)
        {
            _cache.Remove(GetCacheKeyForAllSubscribers());
            _cache.Remove(GetCacheKeyForSubscription(assetHolder, contract));
        }
    }
}