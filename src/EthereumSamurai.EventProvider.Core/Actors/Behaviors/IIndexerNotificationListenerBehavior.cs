using System;
using EthereumSamurai.EventProvider.Core.Messages;

namespace EthereumSamurai.EventProvider.Core.Actors.Behaviors
{
    public interface IIndexerNotificationListenerBehavior
    {
        void Process(IndexerNotificationReceived message, Action<BlockBalancesIndexed> notifyAboutIndexedBlockBalancesAction, Action<BlockIndexed> notifyAboutIndexedBlockAction);

        void StartListening(Action<IndexerNotificationReceived> handleIndexerNotificationAction);

        void StopListening();
    }
}