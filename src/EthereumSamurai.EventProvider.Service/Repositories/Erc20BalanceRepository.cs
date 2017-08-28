namespace EthereumSamurai.EventProvider.Service.Repositories
{
    using System.Collections.Generic;
    using Entities;
    using MongoDB.Driver;
    using Queries;



    public class Erc20BalanceRepository : IErc20BalanceRepository
    {
        private const string BalanceCollectionName = "Erc20BalanceCollection";

        private readonly IMongoCollection<Erc20BalanceEntity>        _balances;
        private readonly FilterDefinitionBuilder<Erc20BalanceEntity> _filterBuilder;



        public Erc20BalanceRepository(IMongoDatabase database)
        {
            _balances      = database.GetCollection<Erc20BalanceEntity>(BalanceCollectionName);
            _filterBuilder = Builders<Erc20BalanceEntity>.Filter;

            CreateIndexes();
        }

        private void CreateIndexes()
        {
            var indexKeys = Builders<Erc20BalanceEntity>.IndexKeys;

            _balances.Indexes.CreateMany(new []
            {
                new CreateIndexModel<Erc20BalanceEntity>
                (
                    indexKeys.Combine
                    (
                        indexKeys.Ascending(x => x.AssetHolderAddress),
                        indexKeys.Ascending(x => x.ContractAddress)
                    )
                ),
                new CreateIndexModel<Erc20BalanceEntity>
                (
                    indexKeys.Ascending(x => x.BlockNumber)
                )
            });
        }


        
        public IEnumerable<Erc20BalanceEntity> Get(Erc20BalancesQuery query)
        {
            var filter = _filterBuilder.Empty;

            if (!string.IsNullOrEmpty(query.AssetHolder))
            {
                filter &= _filterBuilder.Eq(x => x.AssetHolderAddress, query.AssetHolder);
            }
            
            if (query.Contracts != null && query.Contracts.Length > 0)
            {
                filter &= _filterBuilder.In(x => x.ContractAddress, query.Contracts);
            }
            
            if (query.BlockNumber.HasValue)
            {
                filter &= _filterBuilder.Eq(x => x.BlockNumber, query.BlockNumber.Value);
            }

            return _balances
                .Find(filter)
                .ToEnumerable();
        }
    }
}