namespace EthereumSamurai.EventProvider.Service.Repositories
{
    using System.Collections.Generic;
    using Entities;
    using Queries;

    public interface IErc20BalanceRepository
    {
        IEnumerable<Erc20BalanceEntity> Get(Erc20BalancesQuery query);
    }
}