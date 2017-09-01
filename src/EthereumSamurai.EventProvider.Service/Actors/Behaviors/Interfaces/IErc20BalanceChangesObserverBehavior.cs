namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors.Interfaces
{
    using System;
    using Messages;

    internal interface IErc20BalanceChangesObserverBehavior
    {
        void Process(BlockBalancesIndexed message, Action<Notify> sendNotificationAction);
    }
}