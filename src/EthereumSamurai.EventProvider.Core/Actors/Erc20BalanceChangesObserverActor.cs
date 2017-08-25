namespace EthereumSamurai.EventProvider.Core.Actors
{
    using Akka.Actor;
    using Behaviors;
    using Messages;
    using Utils;



    public sealed class Erc20BalanceChangesObserverActor : ReceiveActor
    {
        private readonly IErc20BalanceChangesObserverBehavior _behavior;
        private readonly ICanTell                             _susbcribersNotifier;


        public Erc20BalanceChangesObserverActor(IErc20BalanceChangesObserverBehavior behavior)
        {
            _behavior            = behavior;
            _susbcribersNotifier = Context.ActorSelection(ActorPaths.SubscribersNotifier);

            Receive<BlockBalancesIndexed>(
                msg => Process(msg));
        }

        private void Process(BlockBalancesIndexed message)
        {
            _behavior.Process(message, x => _susbcribersNotifier.Tell(x, Self));
        }
    }
}