namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using System;
    using Messages;

    public interface IErc20BalanceChangesReplayManagerBehavior
    {
        void Process(ReplayErc20BalanceChanges message, Action<Notify> sendNotificationAction);
    }
}