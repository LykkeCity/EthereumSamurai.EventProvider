namespace EthereumSamurai.EventProvider.Core.Actors.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Messages;
    using Notifications;
    using Repositories;
    using Repositories.Queries;
    using Utils;

    public sealed class Erc20BalanceChangesReplayManagerBehavior : IErc20BalanceChangesReplayManagerBehavior
    {
        private readonly IErc20BalanceRepository _balances;

        public Erc20BalanceChangesReplayManagerBehavior(
            IErc20BalanceRepository balances)
        {
            _balances = balances;
        }

        public void Process(ReplayErc20BalanceChanges message, Action<Notify> sendNotificationAction)
        {
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
                    replayNumber:       message.ReplayNumber
                );

                notifications.EnqueueUnicast(changeNotification, message.Exchange, message.RoutingKey);
            }

            // If it is replay, initiated with replay number, preparing replay end notification

            if (message.ReplayNumber.HasValue)
            {
                var replayEndNotification = new Erc20BalanceChangeReplayEndNotification
                (
                    replayNumber: message.ReplayNumber.Value
                );

                notifications.EnqueueUnicast(replayEndNotification, message.Exchange, message.RoutingKey);
            }

            // Sending notifications with specified delegate

            notifications.ForEachDequeue(sendNotificationAction);
        }
    }
}