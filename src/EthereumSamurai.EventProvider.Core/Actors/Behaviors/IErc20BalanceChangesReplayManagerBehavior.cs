using System;
using EthereumSamurai.EventProvider.Core.Messages;

namespace EthereumSamurai.EventProvider.Core.Actors.Behaviors
{
    public interface IErc20BalanceChangesReplayManagerBehavior
    {
        void Process(ReplayErc20BalanceChanges message, Action<Notify> sendNotificationAction);
    }
}