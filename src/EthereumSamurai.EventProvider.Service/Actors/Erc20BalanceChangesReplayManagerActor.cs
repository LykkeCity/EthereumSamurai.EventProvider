namespace EthereumSamurai.EventProvider.Service.Actors
{
    using Akka.Actor;
    using Behaviors.Interfaces;
    using Messages;


    internal sealed class Erc20BalanceChangesReplayManagerActor : ReceiveActor
    {
        private readonly IErc20BalanceChangesReplayManagerBehavior _behavior;
        private readonly ICanTell                                  _susbcribersNotifier;
        


        public Erc20BalanceChangesReplayManagerActor(
            IErc20BalanceChangesReplayManagerBehavior behavior)
        {
            _behavior            = behavior;
            _susbcribersNotifier = Context.ActorSelection(ActorPaths.SubscribersNotifier.Path);
            
            Receive<ReplayErc20BalanceChanges>(
                msg => Process(msg));
        }

        private void Process(ReplayErc20BalanceChanges message)
        {
            _behavior.Process(message, x => _susbcribersNotifier.Tell(x, Self));
        }
    }
}