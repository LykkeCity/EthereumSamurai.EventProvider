namespace EthereumSamurai.EventProvider.Service.Actors
{
    /// <summary>
    ///    Static helper class, used to define paths to fixed-name actors
    /// </summary>
    public static class ActorPaths
    {
        static ActorPaths()
        {
            Erc20BalanceChangesObserver             = new ActorMetadata(nameof(Erc20BalanceChangesObserver));
            Erc20BalanceChangesReplayManager        = new ActorMetadata(nameof(Erc20BalanceChangesReplayManager));
            Erc20BalanceChangesSubscriptionManager  = new ActorMetadata(nameof(Erc20BalanceChangesSubscriptionManager));
            Erc20TransferCommitsObserver            = new ActorMetadata(nameof(Erc20TransferCommitsObserver));
            Erc20TransferCommitsReplayManager       = new ActorMetadata(nameof(Erc20TransferCommitsReplayManager));
            Erc20TransferCommitsSubscriptionManager = new ActorMetadata(nameof(Erc20TransferCommitsSubscriptionManager));
            IndexerNotificationsListener            = new ActorMetadata(nameof(IndexerNotificationsListener));
            SubscribersNotifier                     = new ActorMetadata(nameof(SubscribersNotifier));
        }

        

        public static ActorMetadata Erc20BalanceChangesObserver { get; }

        public static ActorMetadata Erc20BalanceChangesReplayManager { get; }

        public static ActorMetadata Erc20BalanceChangesSubscriptionManager { get; }

        public static ActorMetadata Erc20TransferCommitsObserver { get; }

        public static ActorMetadata Erc20TransferCommitsReplayManager { get; }

        public static ActorMetadata Erc20TransferCommitsSubscriptionManager { get; }

        public static ActorMetadata IndexerNotificationsListener { get; }

        public static ActorMetadata SubscribersNotifier { get; }
    }
}