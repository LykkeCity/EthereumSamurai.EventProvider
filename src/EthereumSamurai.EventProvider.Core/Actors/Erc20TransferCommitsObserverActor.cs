namespace EthereumSamurai.EventProvider.Core.Actors
{
    using System.Collections.Generic;
    using System.Linq;
    using Akka.Actor;
    using Messages;
    using Notifications;
    using Repositories;
    using Repositories.Queries;
    using Repositories.Utils;


    public sealed class Erc20TransferCommitsObserverActor : ReceiveActor
    {
        private readonly IErc20TransferHistoryRepository       _transfers;
        private readonly ICanTell                              _susbcribersNotifier;
        private readonly IErc20TransfersSubscriptionRepository _subscriptions;


        public Erc20TransferCommitsObserverActor(
            IErc20TransferHistoryRepository transfers,
            IErc20TransfersSubscriptionRepository subscriptions)
        {
            _transfers           = transfers;
            _susbcribersNotifier = Context.ActorSelection(ActorPaths.SubscribersNotifier.Path);
            _subscriptions       = subscriptions;

            Receive<BlockIndexed>(
                msg => Process(msg));
        }

        private void Process(BlockIndexed message)
        {
            var notifications  = new List<Notify>();
            var blockTransfers = _transfers.Get(new Erc20TransferHistoriesQuery
            {
                BlockNumber = message.BlockNumber
            });

            // Preparing transfer commit cancel notifications for all active subscribers

            var commitCancelNotification = new Erc20TransferCommitCancelNotification
            (
                blockNumber: message.BlockNumber
            );
            
            notifications.AddRange(commitCancelNotification, _subscriptions.GetSubscribers());

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
                    replayNumber:     default(int?),
                    to:               transfer.To,
                    transactionHash:  transfer.TransactionHash,
                    transactionIndex: transfer.TransactionIndex,
                    transferAmount:   transfer.TransferAmount
                );

                var subscribers = _subscriptions
                    .GetSubscribers(transfer.From, transfer.ContractAddress)
                    .Union(_subscriptions.GetSubscribers(transfer.To, transfer.ContractAddress));
                

                notifications.AddRange(commitNotification, subscribers);
            }

            // Sending notifications via SubscriberNotifier actor

            notifications.ForEach(x => _susbcribersNotifier.Tell(x, Self));
        }
    }
}