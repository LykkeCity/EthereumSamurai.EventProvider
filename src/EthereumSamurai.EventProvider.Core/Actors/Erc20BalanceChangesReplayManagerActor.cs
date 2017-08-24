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



    public sealed class Erc20BalanceChangesReplayManagerActor : ReceiveActor
    {
        private readonly IErc20BalanceRepository _balances;
        private readonly ICanTell                _susbcribersNotifier;
        


        public Erc20BalanceChangesReplayManagerActor(
            IErc20BalanceRepository balances)
        {
            _balances            = balances;
            _susbcribersNotifier = Context.ActorSelection(ActorPaths.SubscribersNotifier.Path);
            
            Receive<ReplayErc20BalanceChanges>(
                msg => Process(msg));
        }

        private void Process(ReplayErc20BalanceChanges message)
        {
            var notifications = new List<Notify>();
            var balances      = _balances.Get(new Erc20BalancesQuery
            {
                AssetHolder = message.AssetHolder,
                Contracts   = message.Contracts.ToArray()
            });

            // Preparing change notification for specified subscriber

            foreach (var balance in balances)
            {
                var changeNotification = new Erc20BalanceChangeNotification
                (
                    assetHolderAddress: balance.AssetHolderAddress,
                    balance:            balance.Balance,
                    blockNumber:        balance.BlockNumber,
                    contractAddress:    balance.ContractAddress,
                    replayNumber:       message.ReplayNumber
                );

                notifications.Add(changeNotification, message.Exchange, message.RoutingKey);
            }

            // If it is replay, initiated with replay number, preparing replay end notification

            if (message.ReplayNumber.HasValue)
            {
                var replayEndNotification = new Erc20BalanceChangeReplayEndNotification
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