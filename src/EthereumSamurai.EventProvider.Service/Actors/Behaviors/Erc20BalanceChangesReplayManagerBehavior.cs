namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Actors.Extensions;
    using Interfaces;
    using Messages;
    using Notifications;
    using Repositories.Interfaces;
    using Repositories.Queries;

    
    internal sealed class Erc20BalanceChangesReplayManagerBehavior : IErc20BalanceChangesReplayManagerBehavior
    {
        private readonly IErc20BalanceRepository _balances;

        public Erc20BalanceChangesReplayManagerBehavior(
            IErc20BalanceRepository balances)
        {
            _balances = balances;
        }

        public void Process(ReplayErc20BalanceChanges message, Action<Notify> sendNotificationAction)
        {
            // TODO: Ensure, that subscriber exists

            var notifications = new Queue<Notify>();
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
                    replayId:           message.ReplayId
                );

                notifications.EnqueueUnicast(changeNotification, message.Exchange, message.RoutingKey);
            }

            // If it is replay, initiated with replay id, preparing replay end notification

            if (message.ReplayId.HasValue)
            {
                var replayEndNotification = new Erc20BalanceChangeReplayEndNotification
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