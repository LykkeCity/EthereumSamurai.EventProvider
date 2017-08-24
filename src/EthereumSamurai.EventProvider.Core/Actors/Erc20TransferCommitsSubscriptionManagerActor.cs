namespace EthereumSamurai.EventProvider.Core.Actors
{
    using Akka.Actor;
    using Messages;
    using Repositories;
    using Utils;



    public sealed class Erc20TransferCommitsSubscriptionManagerActor : ReceiveActor
    {
        private readonly ICanTell                              _replayManager;
        private readonly IErc20TransfersSubscriptionRepository _subscriptions;



        public Erc20TransferCommitsSubscriptionManagerActor(
            IErc20TransfersSubscriptionRepository subscriptions)
        {
            _replayManager = Context.ActorSelection(ActorPaths.Erc20TransferCommitsReplayManager);
            _subscriptions = subscriptions;

            Receive<SubscribeToErc20TransferCommits>(
                msg => Process(msg));

            Receive<UnsubscribeFromErc20TransferCommits>(
                msg => Process(msg));
        }

        private void Process(SubscribeToErc20TransferCommits message)
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

            _replayManager.Tell(new ReplayErc20TransferCommits
            (
                exchange:     message.Exchange,
                routingKey:   message.RoutingKey,
                replayNumber: default(int?),
                assetHolders: new[] { message.AssetHolder },
                contracts:    message.Contracts
            ), Self);
        }

        private void Process(UnsubscribeFromErc20TransferCommits message)
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