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
    using Utils;



    public sealed class Erc20TransferCommitsReplayManagerActor : ReceiveActor
    {
        private readonly ICanTell                        _susbcribersNotifier;
        private readonly IErc20TransferHistoryRepository _transfers;

        public Erc20TransferCommitsReplayManagerActor(
            IErc20TransferHistoryRepository transfers)
        {
            _susbcribersNotifier = Context.ActorSelection(ActorPaths.SubscribersNotifier);
            _transfers           = transfers;

            Receive<ReplayErc20TransferCommits>(
                msg => Process(msg));
        }

        private void Process(ReplayErc20TransferCommits message)
        {
            var notifications = new List<Notify>();
            var transfers     = _transfers.Get(new Erc20TransferHistoriesQuery
            {
                AssetHolders  = message.AssetHolders.ToArray(),
                Contracts     = message.Contracts.ToArray()
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
                    replayNumber:     message.ReplayNumber,
                    to:               transfer.To,
                    transactionHash:  transfer.TransactionHash,
                    transactionIndex: transfer.TransactionIndex,
                    transferAmount:   transfer.TransferAmount
                );

                notifications.Add(transferCommitNotification, message.Exchange, message.RoutingKey);
            }

            // If it is replay, initiated with replay number, preparing replay end notification

            if (message.ReplayNumber.HasValue)
            {
                var replayEndNotification = new Erc20TransferCommitReplayEndNotification
                (
                    replayNumber: message.ReplayNumber.Value
                );

                notifications.Add(replayEndNotification, message.Exchange, message.RoutingKey);
            }

            // Sending notifications via SubscriberNotifier actor

            notifications.ForEach(x => _susbcribersNotifier.Tell(x, Self));
        }
    }
}