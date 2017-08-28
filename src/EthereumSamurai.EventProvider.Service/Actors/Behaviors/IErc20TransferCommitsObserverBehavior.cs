using System;
using EthereumSamurai.EventProvider.Service.Messages;

namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using Messages;

    public interface IErc20TransferCommitsObserverBehavior
    {
        void Process(BlockIndexed message, Action<Notify> sendNotificationAction);
    }
}