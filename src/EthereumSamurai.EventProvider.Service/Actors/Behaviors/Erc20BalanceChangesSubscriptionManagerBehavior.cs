namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using System;
    using System.Linq;
    using Interfaces;
    using Messages;
    
    using ISubscriptionRepository = Repositories.Interfaces.IErc20SubscriptionRepository<Repositories.Entities.Erc20BalanceChangesSubscriptionEntity>;



    internal sealed class Erc20BalanceChangesSubscriptionManagerBehavior : IErc20BalanceChangesSubscriptionManagerBehavior
    {
        private readonly ISubscriptionRepository _subscriptions;

        public Erc20BalanceChangesSubscriptionManagerBehavior(
            ISubscriptionRepository subscriptions)
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