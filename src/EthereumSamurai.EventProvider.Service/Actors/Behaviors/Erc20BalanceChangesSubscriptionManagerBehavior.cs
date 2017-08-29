namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using System;
    using System.Linq;
    using Messages;
    using Repositories;

    public sealed class Erc20BalanceChangesSubscriptionManagerBehavior : IErc20BalanceChangesSubscriptionManagerBehavior
    {
        private readonly IErc20SubscriptionRepository<IErc20BalanceChanges> _subscriptions;

        public Erc20BalanceChangesSubscriptionManagerBehavior(
            IErc20SubscriptionRepository<IErc20BalanceChanges> subscriptions)
        {
            _subscriptions = subscriptions;
        }

        public void Process(SubscribeToErc20BalanceChanges message, Action<ReplayErc20BalanceChanges> requestReplayAction)
        {
            if (message.Contracts.Any())
            {
                foreach (var contract in message.Contracts)
                {
                    _subscriptions.Subscribe
                    (
                        exchange:    message.Exchange,
                        routingKey:  message.RoutingKey,
                        assetHolder: message.AssetHolder,
                        contract:    contract
                    );
                }
            }
            else
            {
                _subscriptions.Subscribe
                (
                    exchange:    message.Exchange,
                    routingKey:  message.RoutingKey,
                    assetHolder: message.AssetHolder
                );
            }

            requestReplayAction(new ReplayErc20BalanceChanges
            (
                exchange:    message.Exchange,
                routingKey:  message.RoutingKey,
                assetHolder: message.AssetHolder,
                contracts:   message.Contracts
            ));
        }

        public void Process(UnsubscribeFromErc20BalanceChanges message)
        {
            if (message.Contracts.Any())
            {

                foreach (var contract in message.Contracts)
                {
                    _subscriptions.Unsubscribe
                    (
                        exchange:    message.Exchange,
                        routingKey:  message.RoutingKey,
                        assetHolder: message.AssetHolder,
                        contract:    contract
                    );
                }
            }
            else
            {
                _subscriptions.Unsubscribe
                (
                    exchange:    message.Exchange,
                    routingKey:  message.RoutingKey,
                    assetHolder: message.AssetHolder
                );
            }
        }
    }
}