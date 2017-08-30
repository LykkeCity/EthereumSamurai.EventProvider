namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using System;
    using System.Collections.Generic;
    using Messages;
    using Notifications;
    using Repositories;
    using Repositories.Queries;
    using Utils;


    using ISubscriptionRepository = Repositories.IErc20SubscriptionRepository<Repositories.Subscriptions.IErc20BalanceChangesSubscription>;


    public sealed class Erc20BalanceChangesObserverBehavior : IErc20BalanceChangesObserverBehavior
    {
        private readonly IErc20BalanceRepository _balances;
        private readonly ISubscriptionRepository _subscriptions;


        public Erc20BalanceChangesObserverBehavior(
            IErc20BalanceRepository balances,
            ISubscriptionRepository subscriptions)
        {
            _balances      = balances;
            _subscriptions = subscriptions;
        }


        public void Process(BlockBalancesIndexed message, Action<Notify> sendNotificationAction)
        {
            var notifications = new Queue<Notify>();
            var blockBalances = _balances.Get(new Erc20BalancesQuery
            {
                BlockNumber = message.BlockNumber
            });

            // Preparing balance change cancel notifications for all active subscribers

            var changeCancelNotification = new Erc20BalanceChangeCancelNotification
            (
                fromBlockNumber: message.BlockNumber
            );

            notifications.EnqueueMulticastRange(changeCancelNotification, _subscriptions.GetSubscribers());

            // Preparing change notifications for those, who subscribed to that changes

            foreach (var balance in blockBalances)
            {
                var changeNotification = new Erc20BalanceChangeNotification
                (
                    assetHolderAddress: balance.AssetHolderAddress,
                    balance:            balance.Balance,
                    blockNumber:        balance.BlockNumber,
                    contractAddress:    balance.ContractAddress,
                    replayId:           default(int?)
                );

                var balanceSubscribers = _subscriptions.GetSubscribers
                (
                    balance.AssetHolderAddress,
                    balance.ContractAddress
                );

                notifications.EnqueueMulticastRange(changeNotification, balanceSubscribers);
            }

            // Sending notifications with specified delegate
            
            notifications.ForEachDequeue(sendNotificationAction);
        }
    }
}