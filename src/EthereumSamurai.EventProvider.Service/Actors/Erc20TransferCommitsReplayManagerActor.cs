namespace EthereumSamurai.EventProvider.Service.Actors
{
    using Akka.Actor;
    using Behaviors.Interfaces;
    using Extensions;
    using Messages;


    internal sealed class Erc20TransferCommitsReplayManagerActor : ReceiveActor
    {
        private readonly IErc20TransferCommitsReplayManagerBehavior _behavior;
        private readonly ICanTell                                   _susbcribersNotifier;

        public Erc20TransferCommitsReplayManagerActor(
            IErc20TransferCommitsReplayManagerBehavior behavior)
        {
            _behavior            = behavior;
            _susbcribersNotifier = Context.ActorSelection(ActorPaths.SubscribersNotifier);

            Receive<ReplayErc20TransferCommits>(
                msg => Process(msg));
        }

        private void Process(ReplayErc20TransferCommits message)
        {
            _behavior.Process(message, x => _susbcribersNotifier.Tell(x, Self));
        }
    }
}