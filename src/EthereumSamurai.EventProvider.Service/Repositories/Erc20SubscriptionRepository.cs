namespace EthereumSamurai.EventProvider.Service.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using MongoDB.Driver;

    public class Erc20SubscriptionRepository : IErc20SubscriptionRepository
    {
        private const char Delimiter = '|';

        private readonly FilterDefinitionBuilder<Erc20SubscriptionEntity> _filterBuilder;
        private readonly IMongoCollection<Erc20SubscriptionEntity> _subscriptions;



        public Erc20SubscriptionRepository(IMongoDatabase database, string collectionName)
        {
            CollectionName = collectionName;
            _filterBuilder = Builders<Erc20SubscriptionEntity>.Filter;
            _subscriptions = database.GetCollection<Erc20SubscriptionEntity>(CollectionName);

            CreateIndexes();
        }


        public string CollectionName { get; }


        public IEnumerable<(string exchange, string routingKey)> GetSubscribers()
        {
            var filter = _filterBuilder.Empty;

            return GetSubscribers(filter);
        }
        
        public IEnumerable<(string exchange, string routingKey)> GetSubscribers(string assetHolder, string contract)
        {
            var filter = _filterBuilder.Empty;

            filter &= _filterBuilder.Or
            (
                _filterBuilder.Eq(x => x.AssetHolder, assetHolder),
                _filterBuilder.Eq(x => x.AssetHolder, "*")
            );

            filter &= _filterBuilder.Or
            (
                _filterBuilder.Eq(x => x.Contract, contract),
                _filterBuilder.Eq(x => x.Contract, "*")
            );

            return GetSubscribers(filter);
        }

        public IEnumerable<(string exchange, string routingKey)> GetSubscribers(string[] assetHolders, string contract)
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

        public IEnumerable<(string exchange, string routingKey)> GetSubscribers(string assetHolder, string[] contracts)
        {
            var filter = _filterBuilder.Empty;

            filter &= _filterBuilder.Or
            (
                _filterBuilder.Eq(x => x.AssetHolder, assetHolder),
                _filterBuilder.Eq(x => x.AssetHolder, "*")
            );

            filter &= _filterBuilder.In(x => x.Contract, contracts);

            return GetSubscribers(filter);
        }

        public void Subscribe(string exchange, string routingKey, string assetHolder, string contract)
        {
            Unsubscribe(exchange, routingKey, assetHolder, contract);

            _subscriptions.InsertOne(new Erc20SubscriptionEntity
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
            var indexKeys = Builders<Erc20SubscriptionEntity>.IndexKeys;

            _subscriptions.Indexes.CreateMany(new[]
            {
                new CreateIndexModel<Erc20SubscriptionEntity>
                (
                    indexKeys.Combine
                    (
                        indexKeys.Ascending(x => x.Subscriber),
                        indexKeys.Ascending(x => x.AssetHolder),
                        indexKeys.Ascending(x => x.Contract)
                    )
                ),
                new CreateIndexModel<Erc20SubscriptionEntity>
                (
                    indexKeys.Combine
                    (
                        indexKeys.Ascending(x => x.Subscriber),
                        indexKeys.Ascending(x => x.Contract)
                    )
                ),
                new CreateIndexModel<Erc20SubscriptionEntity>
                (
                    indexKeys.Combine
                    (
                        indexKeys.Ascending(x => x.AssetHolder),
                        indexKeys.Ascending(x => x.Contract)
                    )
                )
            });
        }

        private IEnumerable<(string exchange, string routingKey)> GetSubscribers(FilterDefinition<Erc20SubscriptionEntity> filter)
        {
            return _subscriptions
                .Distinct(x => x.Subscriber, filter)
                .ToEnumerable()
                .Select(x => x.Split(Delimiter))
                .Select(x => (x[0], x[1]));
        }
    }
}