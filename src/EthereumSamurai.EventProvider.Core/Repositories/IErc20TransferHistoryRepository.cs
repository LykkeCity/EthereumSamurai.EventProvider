namespace EthereumSamurai.EventProvider.Core.Repositories
{
    using System.Collections.Generic;
    using Entities;
    using Queries;

    public interface IErc20TransferHistoryRepository
    {
        IEnumerable<Erc20TransferHistoryEntity> Get(Erc20TransferHistoriesQuery query);
    }
}