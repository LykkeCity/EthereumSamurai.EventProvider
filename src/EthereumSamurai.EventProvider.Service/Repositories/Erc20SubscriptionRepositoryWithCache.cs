namespace EthereumSamurai.EventProvider.Service.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entities.Interfaces;
    using Interfaces;
    using Microsoft.Extensions.Caching.Memory;



    public class Erc20SubscriptionRepositoryWithCache<T> : IErc20SubscriptionRepository<T>
        where T : IErc20SubscriptionEntity
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

        public IEnumerable<(string exchange, string routingKey)> GetSubscribers(string contract, params string[] assetHolders)
        {
            // TODO: Imrove this caching strategy

            return assetHolders
                .SelectMany(x =>
                {
                    return _cache.GetOrCreate
                    (
                        GetCacheKeyForSubscription(x, contract),
                        y => ConfigureCacheEntry(y, _repository.GetSubscribers(x, contract))
                    );
                })
                .Distinct();
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
        
        private IEnumerable<(string exchange, string routingKey)> ConfigureCacheEntry(ICacheEntry entry, IEnumerable<(string exchange, string routingKey)> value)
        {
            entry.AbsoluteExpirationRelativeToNow = _cacheDuration;
            entry.Value                           = value;

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