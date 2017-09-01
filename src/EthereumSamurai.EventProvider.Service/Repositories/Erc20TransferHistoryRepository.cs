namespace EthereumSamurai.EventProvider.Service.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ApiClient;
    using ApiClient.Models;
    using Entities;
    using Interfaces;
    using Queries;



    public class Erc20TransferHistoryRepository : IErc20TransferHistoryRepository
    {
        private readonly IEthereumSamuraiApi _api;


        public Erc20TransferHistoryRepository(IEthereumSamuraiApi api)
        {
            _api = api;
        }

        public IEnumerable<Erc20TransferHistoryEntity> Get(Erc20TransferHistoriesQuery query)
        {
            // Keep in mind, that currently AutoRest does not generate unsigned number types

            const int pageSize = 100;
            var pageNumber = 0;

            while (true)
            {
                var transfersResponse = _api.ApiErc20TransferHistoryGetErc20TransfersPost
                (
                    request: new GetErc20TransferHistoryRequest
                    {
                        AssetHolder = query.AssetHolder,
                        BlockNumber = (long?) query.BlockNumber,
                        Contracts   = query.Contracts
                    },
                    start: pageNumber * pageSize,
                    count: pageSize
                );

                if (transfersResponse is IEnumerable<Erc20TransferHistoryResponse> transfersPage)
                {
                    var transfers = transfersPage.Select(x => new Erc20TransferHistoryEntity
                    {
                        BlockHash        = x.BlockHash,
                        BlockNumber      = (ulong) x.BlockNumber,
                        BlockTimestamp   = (ulong) x.BlockTimestamp,
                        ContractAddress  = x.ContractAddress,
                        From             = x.FromProperty,
                        LogIndex         = (uint) x.LogIndex,
                        To               = x.To,
                        TransactionHash  = x.TransactionHash,
                        TransactionIndex = (uint) x.TransactionIndex,
                        TransferAmount   = x.TransferAmount
                    }).ToList();

                    foreach (var transfer in transfers)
                    {
                        yield return transfer;
                    }

                    if (transfers.Count < pageSize)
                    {
                        break;
                    }
                    else
                    {
                        pageNumber++;
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