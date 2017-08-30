namespace EthereumSamurai.EventProvider.Service.Actors
{
    using Akka.Actor;
    using Behaviors;
    using Messages;
    using Utils;



    public sealed class IndexerNotificationsListenerActor : ReceiveActor
    {
        private readonly IIndexerNotificationsListenerBehavior _behavior;
        private readonly ICanTell                             _erc20BalanceChangesObserver;
        private readonly ICanTell                             _erc20TransferCommitsObserver;


        public IndexerNotificationsListenerActor(
            IIndexerNotificationsListenerBehavior behavior)
        {
            _behavior                     = behavior;
            _erc20BalanceChangesObserver  = Context.ActorSelection(ActorPaths.Erc20BalanceChangesObserver);
            _erc20TransferCommitsObserver = Context.ActorSelection(ActorPaths.Erc20TransferCommitsObserver);


            Receive<IndexerNotificationReceived>(
                msg => Process(msg));
        }

        protected override void PostStop()
        {
            _behavior.StopListening();

            base.PostStop();
        }

        protected override void PreStart()
        {
            var self = Context.Self; // It's important to use clojure!

            _behavior.StartListening(msg =>
            {
                self.Tell(msg, ActorRefs.Nobody);
            });

            base.PreStart();
        }

        private void NotifyAboutIndexedBlock(BlockIndexed message)
        {
            _erc20BalanceChangesObserver.Tell(message, Self);
        }

        private void NotifyAboutIndexedBlockBalances(BlockBalancesIndexed message)
        {
            _erc20TransferCommitsObserver.Tell(message, Self);
        }

        private void Process(IndexerNotificationReceived message)
        {
            _behavior.Process(message, NotifyAboutIndexedBlockBalances, NotifyAboutIndexedBlock);
        }
    }
}