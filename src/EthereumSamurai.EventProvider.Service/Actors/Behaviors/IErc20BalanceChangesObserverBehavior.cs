namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using System;
    using Messages;


    public interface IErc20BalanceChangesObserverBehavior
    {
        void Process(BlockBalancesIndexed message, Action<Notify> sendNotificationAction);
    }
}