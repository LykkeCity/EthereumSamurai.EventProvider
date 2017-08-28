namespace EthereumSamurai.EventProvider.Service.Repositories
{
    using System.Collections.Generic;
    using Entities;
    using MongoDB.Driver;
    using Queries;



    public class Erc20TransferHistoryRepository : IErc20TransferHistoryRepository
    {
        private const string BalanceCollectionName = "Erc20TransferHistoryCollection";

        private readonly IMongoCollection<Erc20TransferHistoryEntity>        _transfers;
        private readonly FilterDefinitionBuilder<Erc20TransferHistoryEntity> _filterBuilder;

        public Erc20TransferHistoryRepository(IMongoDatabase database)
        {
            _transfers     = database.GetCollection<Erc20TransferHistoryEntity>(BalanceCollectionName);
            _filterBuilder = Builders<Erc20TransferHistoryEntity>.Filter;

            CreateIndexes();
        }


        private void CreateIndexes()
        {
            var indexKeys = Builders<Erc20TransferHistoryEntity>.IndexKeys;

            _transfers.Indexes.CreateMany(new[]
            {
                new CreateIndexModel<Erc20TransferHistoryEntity>
                (
                    indexKeys.Combine
                    (
                        indexKeys.Ascending(x => x.BlockNumber),
                        indexKeys.Ascending(x => x.From),
                        indexKeys.Ascending(x => x.To),
                        indexKeys.Ascending(x => x.ContractAddress)
                    )
                ),
                new CreateIndexModel<Erc20TransferHistoryEntity>
                (
                    indexKeys.Combine
                    (
                        indexKeys.Ascending(x => x.From),
                        indexKeys.Ascending(x => x.To),
                        indexKeys.Ascending(x => x.ContractAddress)
                    )
                ),
                new CreateIndexModel<Erc20TransferHistoryEntity>
                (
                    indexKeys.Combine
                    (
                        indexKeys.Ascending(x => x.BlockNumber),
                        indexKeys.Ascending(x => x.ContractAddress)
                    )
                ),
                new CreateIndexModel<Erc20TransferHistoryEntity>
                (
                    indexKeys.Combine
                    (
                        indexKeys.Ascending(x => x.ContractAddress)
                    )
                )
            });
        }

        public IEnumerable<Erc20TransferHistoryEntity> Get(Erc20TransferHistoriesQuery query)
        {
            var filter = _filterBuilder.Empty;

            if (query.BlockNumber.HasValue)
            {
                filter &= _filterBuilder.Eq(x => x.BlockNumber, query.BlockNumber.Value);
            }
            else
            {
                if (query.FromBlockNumber.HasValue)
                {
                    filter &= _filterBuilder.Gte(x => x.BlockNumber, query.FromBlockNumber.Value);
                }

                if (query.ToBlockNumber.HasValue)
                {
                    filter &= _filterBuilder.Lte(x => x.BlockNumber, query.ToBlockNumber.Value);
                }
            }

            if (query.AssetHolders != null && query.AssetHolders.Length > 0)
            {
                filter &= _filterBuilder.Or
                (
                    _filterBuilder.In(x => x.From, query.AssetHolders),
                    _filterBuilder.In(x => x.To,   query.AssetHolders)
                );
            }

            if (query.Contracts != null && query.Contracts.Length > 0)
            {
                filter &= _filterBuilder.In(x => x.ContractAddress, query.Contracts);
            }
            
            return _transfers.Find(filter).ToEnumerable();
        }
    }
}