namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using Messages;
    using System;

    public interface IIndexerNotificationsListenerBehavior
    {
        void Process(IndexerNotificationReceived message, Action<BlockBalancesIndexed> notifyAboutIndexedBlockBalancesAction, Action<BlockIndexed> notifyAboutIndexedBlockAction);

        void StartListening(Action<IndexerNotificationReceived> handleIndexerNotificationAction);

        void StopListening();
    }
}