namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors.Interfaces
{
    using System;
    using Messages;

    internal interface IErc20TransferCommitsObserverBehavior
    {
        void Process(BlockIndexed message, Action<Notify> sendNotificationAction);
    }
}