using System;
using EthereumSamurai.EventProvider.Service.Messages;

namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using Messages;

    public interface IIndexerNotificationListenerBehavior
    {
        void Process(IndexerNotificationReceived message, Action<BlockBalancesIndexed> notifyAboutIndexedBlockBalancesAction, Action<BlockIndexed> notifyAboutIndexedBlockAction);

        void StartListening(Action<IndexerNotificationReceived> handleIndexerNotificationAction);

        void StopListening();
    }
}