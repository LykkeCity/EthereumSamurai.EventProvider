namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors.Interfaces
{
    using System;
    using Messages;

    internal interface IErc20BalanceChangesReplayManagerBehavior
    {
        void Process(ReplayErc20BalanceChanges message, Action<Notify> sendNotificationAction);
    }
}