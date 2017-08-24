namespace EthereumSamurai.EventProvider.Core.Actors
{
    using System.Linq;
    using Akka.Actor;
    using Messages;
    using Repositories;
    using Utils;



    public sealed class Erc20BalanceChangesSubscribtionManagerActor : ReceiveActor
    {
        private readonly ICanTell                                   _replayManager;
        private readonly IErc20BalanceChangesSubscriptionRepository _subscriptions;



        public Erc20BalanceChangesSubscribtionManagerActor(
            IErc20BalanceChangesSubscriptionRepository subscriptions)
        {
            _replayManager = Context.ActorSelection(ActorPaths.Erc20BalanceChangesReplayManager);
            _subscriptions = subscriptions;

            Receive<SubscribeToErc20BalanceChanges>(
                msg => Process(msg));

            Receive<UnsubscribeFromErc20BalanceChanges>(
                msg => Process(msg));
        }

        private void Process(SubscribeToErc20BalanceChanges message)
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
            
            _replayManager.Tell(new ReplayErc20BalanceChanges
            (
                exchange:    message.Exchange,
                routingKey:  message.RoutingKey,
                assetHolder: message.AssetHolder,
                contracts:   message.Contracts
            ), Self);
        }

        private void Process(UnsubscribeFromErc20BalanceChanges message)
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