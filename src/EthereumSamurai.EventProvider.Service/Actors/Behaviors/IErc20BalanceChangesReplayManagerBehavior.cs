using System;
using EthereumSamurai.EventProvider.Service.Messages;

namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using Messages;

    public interface IErc20BalanceChangesReplayManagerBehavior
    {
        void Process(ReplayErc20BalanceChanges message, Action<Notify> sendNotificationAction);
    }
}