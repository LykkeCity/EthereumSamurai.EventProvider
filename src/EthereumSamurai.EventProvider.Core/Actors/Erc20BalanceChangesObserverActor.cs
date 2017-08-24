namespace EthereumSamurai.EventProvider.Core.Actors
{
    using System.Collections.Generic;
    using Akka.Actor;
    using Messages;
    using Notifications;
    using Repositories;
    using Repositories.Queries;
    using Repositories.Utils;
    using Utils;



    public sealed class Erc20BalanceChangesObserverActor : ReceiveActor
    {
        private readonly IErc20BalanceRepository                    _balances;
        private readonly ICanTell                                   _susbcribersNotifier;
        private readonly IErc20BalanceChangesSubscriptionRepository _subscriptions;


        public Erc20BalanceChangesObserverActor(
            IErc20BalanceRepository balances,
            IErc20BalanceChangesSubscriptionRepository subscriptions)
        {
            _balances            = balances;
            _susbcribersNotifier = Context.ActorSelection(ActorPaths.SubscribersNotifier);
            _subscriptions       = subscriptions;

            Receive<BlockBalancesIndexed>(
                msg => Process(msg));
        }

        private void Process(BlockBalancesIndexed message)
        {
            var notifications = new List<Notify>();
            var blockBalances = _balances.Get(new Erc20BalancesQuery
            {
                BlockNumber = message.BlockNumber
            });
            
            // Preparing balance change cancel notifications for all active subscribers

            var changeCancelNotification = new Erc20BalanceChangeCancelNotification
            (
                fromBlockNumber: message.BlockNumber
            );

            notifications.AddRange(changeCancelNotification, _subscriptions.GetSubscribers());
            
            // Preparing change notifications for those, who subscribed to that changes

            foreach (var balance in blockBalances)
            {
                var changeNotification = new Erc20BalanceChangeNotification
                (
                    assetHolderAddress: balance.AssetHolderAddress,
                    balance:            balance.Balance,
                    blockNumber:        balance.BlockNumber,
                    contractAddress:    balance.ContractAddress,
                    replayNumber:       default(int?)
                );
                
                var balanceSubscribers = _subscriptions.GetSubscribers
                (
                    balance.AssetHolderAddress,
                    balance.ContractAddress
                );

                notifications.AddRange(changeNotification, balanceSubscribers);
            }

            // Sending notifications via SubscriberNotifier actor

            notifications.ForEach(x => _susbcribersNotifier.Tell(x, Self));
        }
    }
}