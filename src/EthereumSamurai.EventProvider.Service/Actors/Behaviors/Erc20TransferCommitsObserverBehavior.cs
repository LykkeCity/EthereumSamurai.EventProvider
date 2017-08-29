namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using System;
    using System.Collections.Generic;
    using Messages;
    using Notifications;
    using Repositories;
    using Repositories.Queries;
    using Utils;


    public class Erc20TransferCommitsObserverBehavior : IErc20TransferCommitsObserverBehavior
    {
        private readonly IErc20TransferHistoryRepository                     _transfers;
        private readonly IErc20SubscriptionRepository<IERc20TransferCommits> _subscriptions;

        public Erc20TransferCommitsObserverBehavior(
            IErc20TransferHistoryRepository                     transfers,
            IErc20SubscriptionRepository<IERc20TransferCommits> subscriptions)
        {
            _transfers     = transfers;
            _subscriptions = subscriptions;
        }

        public void Process(BlockIndexed message, Action<Notify> sendNotificationAction)
        {
            var notifications  = new Queue<Notify>();
            var blockTransfers = _transfers.Get(new Erc20TransferHistoriesQuery
            {
                BlockNumber = message.BlockNumber
            });

            // Preparing transfer commit cancel notifications for all active subscribers

            var commitCancelNotification = new Erc20TransferCommitCancelNotification
            (
                blockNumber: message.BlockNumber
            );

            notifications.EnqueueMulticastRange(commitCancelNotification, _subscriptions.GetSubscribers());

            // Preparing commit notifications for those, who subscribed to that changes

            foreach (var transfer in blockTransfers)
            {
                var commitNotification = new Erc20TransferCommitNotification
                (
                    blockHash:        transfer.BlockHash,
                    blockNumber:      transfer.BlockNumber,
                    blockTimestamp:   transfer.BlockTimestamp,
                    contractAddress:  transfer.ContractAddress,
                    from:             transfer.From,
                    logIndex:         transfer.LogIndex,
                    replayId:         default(int?),
                    to:               transfer.To,
                    transactionHash:  transfer.TransactionHash,
                    transactionIndex: transfer.TransactionIndex,
                    transferAmount:   transfer.TransferAmount
                );

                var subscribers = _subscriptions.GetSubscribers
                (
                    assetHolders: new[] { transfer.From, transfer.To },
                    contract:     transfer.ContractAddress
                );
                
                notifications.EnqueueMulticastRange(commitNotification, subscribers);
            }

            // Sending notifications with specified delegate

            notifications.ForEachDequeue(sendNotificationAction);
        }
    }
}