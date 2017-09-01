namespace EthereumSamurai.EventProvider.Service.Actors
{
    using Akka.Actor;
    using Behaviors.Interfaces;
    using Extensions;
    using Messages;


    internal sealed class Erc20BalanceChangesSubscribtionManagerActor : ReceiveActor
    {
        private readonly IErc20BalanceChangesSubscriptionManagerBehavior _behavior;
        private readonly ICanTell                                        _replayManager;



        public Erc20BalanceChangesSubscribtionManagerActor(
            IErc20BalanceChangesSubscriptionManagerBehavior behavior)
        {
            _behavior      = behavior;
            _replayManager = Context.ActorSelection(ActorPaths.Erc20BalanceChangesReplayManager);
            
            Receive<SubscribeToErc20BalanceChanges>(
                msg => Process(msg));

            Receive<UnsubscribeFromErc20BalanceChanges>(
                msg => Process(msg));
        }


        private void Process(SubscribeToErc20BalanceChanges message)
        {
            _behavior.Process(message, x => _replayManager.Tell(x, Self));
        }

        private void Process(UnsubscribeFromErc20BalanceChanges message)
        {
            _behavior.Process(message);
        }
    }
}