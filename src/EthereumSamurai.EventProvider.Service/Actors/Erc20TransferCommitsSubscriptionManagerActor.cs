namespace EthereumSamurai.EventProvider.Service.Actors
{
    using Akka.Actor;
    using Behaviors.Interfaces;
    using Extensions;
    using Messages;


    internal sealed class Erc20TransferCommitsSubscriptionManagerActor : ReceiveActor
    {
        private readonly IErc20TransferCommitsSubscriptionManagerBehavior _behavior;
        private readonly ICanTell                                         _replayManager;



        public Erc20TransferCommitsSubscriptionManagerActor(
            IErc20TransferCommitsSubscriptionManagerBehavior behavior)
        {
            _behavior      = behavior;
            _replayManager = Context.ActorSelection(ActorPaths.Erc20TransferCommitsReplayManager);

            Receive<SubscribeToErc20TransferCommits>(
                msg => Process(msg));

            Receive<UnsubscribeFromErc20TransferCommits>(
                msg => Process(msg));
        }

        private void Process(SubscribeToErc20TransferCommits message)
        {
            _behavior.Process(message, x => _replayManager.Tell(x, Self));
        }

        private void Process(UnsubscribeFromErc20TransferCommits message)
        {
            _behavior.Process(message);
        }
    }
}