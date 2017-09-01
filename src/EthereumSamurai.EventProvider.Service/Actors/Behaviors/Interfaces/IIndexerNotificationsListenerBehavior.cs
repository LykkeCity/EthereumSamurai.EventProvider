namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors.Interfaces
{
    using System;
    using Messages;

    internal interface IIndexerNotificationsListenerBehavior
    {
        void Process(IndexerNotificationReceived message, Action<BlockBalancesIndexed> notifyAboutIndexedBlockBalancesAction, Action<BlockIndexed> notifyAboutIndexedBlockAction);

        void StartListening(Action<IndexerNotificationReceived> handleIndexerNotificationAction);

        void StopListening();
    }
}