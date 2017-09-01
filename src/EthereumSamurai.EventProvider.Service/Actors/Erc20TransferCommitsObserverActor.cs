namespace EthereumSamurai.EventProvider.Service.Actors
{
    using Akka.Actor;
    using Behaviors.Interfaces;
    using Messages;



    internal sealed class Erc20TransferCommitsObserverActor : ReceiveActor
    {
        private readonly IErc20TransferCommitsObserverBehavior _behavior;
        private readonly ICanTell                              _susbcribersNotifier;


        public Erc20TransferCommitsObserverActor(
            IErc20TransferCommitsObserverBehavior behavior)
        {
            _behavior            = behavior;
            _susbcribersNotifier = Context.ActorSelection(ActorPaths.SubscribersNotifier.Path);
            
            Receive<BlockIndexed>(
                msg => Process(msg));
        }

        private void Process(BlockIndexed message)
        {
            _behavior.Process(message, x => _susbcribersNotifier.Tell(x, Self));
        }
    }
}