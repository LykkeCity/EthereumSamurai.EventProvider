using System;
using EthereumSamurai.EventProvider.Service.Messages;

namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using Messages;

    public interface IErc20BalanceChangesObserverBehavior
    {
        void Process(BlockBalancesIndexed message, Action<Notify> sendNotificationAction);
    }
}