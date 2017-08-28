namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Messages;
    using Notifications;
    using Repositories;
    using Repositories.Queries;
    using Utils;



    public sealed class Erc20TransferCommitsReplayManagerBehavior : IErc20TransferCommitsReplayManagerBehavior
    {
        private readonly IErc20TransferHistoryRepository _transfers;

        public Erc20TransferCommitsReplayManagerBehavior(
            IErc20TransferHistoryRepository transfers)
        {
            _transfers = transfers;
        }

        public void Process(ReplayErc20TransferCommits message, Action<Notify> sendNotificationAction)
        {
            var notifications = new Queue<Notify>();
            var transfers     = _transfers.Get(new Erc20TransferHistoriesQuery
            {
                AssetHolders = new[] { message.AssetHolder },
                Contracts    = message.Contracts.ToArray()
            });

            // Preparing change notification for specified subscriber

            foreach (var transfer in transfers)
            {
                var transferCommitNotification = new Erc20TransferCommitNotification
                (
                    blockHash:        transfer.BlockHash,
                    blockNumber:      transfer.BlockNumber,
                    blockTimestamp:   transfer.BlockTimestamp,
                    contractAddress:  transfer.ContractAddress,
                    from:             transfer.From,
                    logIndex:         transfer.LogIndex,
                    replayId:         message.ReplayId,
                    to:               transfer.To,
                    transactionHash:  transfer.TransactionHash,
                    transactionIndex: transfer.TransactionIndex,
                    transferAmount:   transfer.TransferAmount
                );

                notifications.EnqueueUnicast(transferCommitNotification, message.Exchange, message.RoutingKey);
            }

            // If it is replay, initiated with replay id, preparing replay end notification

            if (message.ReplayId.HasValue)
            {
                var replayEndNotification = new Erc20TransferCommitReplayEndNotification
                (
                    replayId: message.ReplayId.Value
                );

                notifications.EnqueueUnicast(replayEndNotification, message.Exchange, message.RoutingKey);
            }

            // Sending notifications with specified delegate

            notifications.ForEachDequeue(sendNotificationAction);
        }
    }
}