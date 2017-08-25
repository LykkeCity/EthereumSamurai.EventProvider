using System;
using EthereumSamurai.EventProvider.Core.Messages;

namespace EthereumSamurai.EventProvider.Core.Actors.Behaviors
{
    public interface IErc20TransferCommitsObserverBehavior
    {
        void Process(BlockIndexed message, Action<Notify> sendNotificationAction);
    }
}