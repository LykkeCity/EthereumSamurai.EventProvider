﻿namespace EthereumSamurai.EventProvider.Service.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ApiClient;
    using ApiClient.Models;
    using Entities;
    using Interfaces;
    using Queries;
    

    public class Erc20BalanceRepository : IErc20BalanceRepository
    {
        private readonly IEthereumSamuraiApi _api;

        public Erc20BalanceRepository(IEthereumSamuraiApi api)
        {
            _api = api;
        }

        
        public IEnumerable<Erc20BalanceEntity> Get(Erc20BalancesQuery query)
        {
            const int pageSize = 1000;
            var pageIndex      = 0;

            while(true)
            {
                //// Keep in mind, that currently (2017-09-01) AutoRest does not generate unsigned number types.
                //// So, unfortunately, we should make explicit casts.

                var balancesResponse = _api.ApiErc20BalanceGetErc20BalancePost
                (
                    request: new GetErc20BalanceRequest
                    {
                        AssetHolder = query.AssetHolder,
                        BlockNumber = (long?) query.BlockNumber,
                        Contracts   = query.Contracts
                    },
                    start: pageIndex * pageSize,
                    count: pageSize
                );

                if (balancesResponse is IEnumerable<Erc20BalanceResponse> balancesPage)
                {
                    var balances = balancesPage.Select(x => new Erc20BalanceEntity
                    {
                        AssetHolderAddress = x.Address,
                        Balance            = x.Amount,
                        BlockNumber        = (ulong) x.BlockNumber,
                        ContractAddress    = x.Contract
                    }).ToList();
                    
                    foreach (var balance in balances)
                    {
                        yield return balance;
                    }

                    if (balances.Count < pageSize)
                    {
                        break;
                    }
                    else
                    {
                        pageIndex++;
                    }
                }
                else
                {
                    // TODO: Add custom exception

                    throw new Exception("EthereumSamurai API returned invalid response.");
                }
            }
        }
    }
}