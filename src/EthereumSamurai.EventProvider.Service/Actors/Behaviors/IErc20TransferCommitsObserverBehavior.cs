namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using System;
    using Messages;

    public interface IErc20TransferCommitsObserverBehavior
    {
        void Process(BlockIndexed message, Action<Notify> sendNotificationAction);
    }
}