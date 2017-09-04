namespace EthereumSamurai.EventProvider.Service.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Entities.Interfaces;
    using Interfaces;
    using MongoDB.Driver;

    
    public class Erc20SubscriptionRepository<T> : IErc20SubscriptionRepository<T>
        where T : IErc20SubscriptionEntity, new()
    {
        private const char Delimiter = '|';

        private readonly FilterDefinitionBuilder<T> _filterBuilder;
        private readonly IMongoCollection<T>        _subscriptions;

        
        public Erc20SubscriptionRepository(
            string         collectionName,
            IMongoDatabase database)
        {
            CollectionName = collectionName;
            _filterBuilder = Builders<T>.Filter;
            _subscriptions = database.GetCollection<T>(CollectionName);

            CreateIndexes();
        }


        public string CollectionName { get; }


        public IEnumerable<(string exchange, string routingKey)> GetSubscribers()
        {
            var filter = _filterBuilder.Empty;

            return GetSubscribers(filter);
        }
        
        public IEnumerable<(string exchange, string routingKey)> GetSubscribers(string contract, params string[] assetHolders)
        {
            var filter = _filterBuilder.Empty;

            filter &= _filterBuilder.In(x => x.AssetHolder, assetHolders);

            filter &= _filterBuilder.Or
            (
                _filterBuilder.Eq(x => x.Contract, contract),
                _filterBuilder.Eq(x => x.Contract, "*")
            );

            return GetSubscribers(filter);
        }

        public void Subscribe(string exchange, string routingKey, string assetHolder, string contract)
        {
            Unsubscribe(exchange, routingKey, assetHolder, contract);

            _subscriptions.InsertOne(new T
            {
                AssetHolder = assetHolder,
                Contract    = contract,
                Subscriber  = GetSubscriber(exchange, routingKey)
            });
        }

        public void Unsubscribe(string exchange, string routingKey, string assetHolder, string contract)
        {
            var subscriber = GetSubscriber(exchange, routingKey);
            var filter     = _filterBuilder.Eq(x => x.Subscriber, subscriber);

            if (assetHolder != "*")
            {
                filter &= _filterBuilder.Eq(x => x.AssetHolder, assetHolder);
            }
            
            if (contract != "*")
            {
                filter &= _filterBuilder.Eq(x => x.Contract, contract);
            }
            
            _subscriptions.DeleteMany(filter);
        }
        
        private static string GetSubscriber(string exchange, string routingKey)
        {
            return $"{exchange}{Delimiter}{routingKey}";
        }
        
        private void CreateIndexes()
        {
            var indexKeys = Builders<T>.IndexKeys;

            _subscriptions.Indexes.CreateMany(new[]
            {
                new CreateIndexModel<T>
                (
                    indexKeys.Combine
                    (
                        indexKeys.Ascending(x => x.Subscriber),
                        indexKeys.Ascending(x => x.AssetHolder),
                        indexKeys.Ascending(x => x.Contract)
                    )
                ),
                new CreateIndexModel<T>
                (
                    indexKeys.Combine
                    (
                        indexKeys.Ascending(x => x.Subscriber),
                        indexKeys.Ascending(x => x.Contract)
                    )
                ),
                new CreateIndexModel<T>
                (
                    indexKeys.Combine
                    (
                        indexKeys.Ascending(x => x.AssetHolder),
                        indexKeys.Ascending(x => x.Contract)
                    )
                )
            });
        }

        private IEnumerable<(string exchange, string routingKey)> GetSubscribers(FilterDefinition<T> filter)
        {
            return _subscriptions
                .Distinct(x => x.Subscriber, filter)
                .ToEnumerable()
                .Select(x => x.Split(Delimiter))
                .Select(x => (x[0], x[1]));
        }
    }
}