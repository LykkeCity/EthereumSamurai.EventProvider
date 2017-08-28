namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using System;
    using Messages;
    using Repositories;

    public sealed class Erc20TransferCommitsSubscriptionManagerBehavior : IErc20TransferCommitsSubscriptionManagerBehavior
    {
        private readonly IErc20TransferCommitsSubscriptionRepository _subscriptions;

        public Erc20TransferCommitsSubscriptionManagerBehavior(
            IErc20TransferCommitsSubscriptionRepository subscriptions)
        {
            _subscriptions = subscriptions;
        }

        public void Process(SubscribeToErc20TransferCommits message, Action<ReplayErc20TransferCommits> requestReplayAction)
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

            requestReplayAction(new ReplayErc20TransferCommits
            (
                exchange:    message.Exchange,
                routingKey:  message.RoutingKey,
                assetHolder: message.AssetHolder,
                contracts:   message.Contracts
            ));
        }

        public void Process(UnsubscribeFromErc20TransferCommits message)
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
    }
}