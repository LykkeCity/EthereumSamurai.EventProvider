using System;
using EthereumSamurai.EventProvider.Core.Messages;

namespace EthereumSamurai.EventProvider.Core.Actors.Behaviors
{
    public interface IErc20BalanceChangesObserverBehavior
    {
        void Process(BlockBalancesIndexed message, Action<Notify> sendNotificationAction);
    }
}