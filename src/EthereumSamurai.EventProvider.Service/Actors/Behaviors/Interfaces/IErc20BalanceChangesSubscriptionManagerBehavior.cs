namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors.Interfaces
{
    using System;
    using Messages;

    internal interface IErc20BalanceChangesSubscriptionManagerBehavior
    {
        void Process(SubscribeToErc20BalanceChanges message, Action<ReplayErc20BalanceChanges> requestReplayAction);

        void Process(UnsubscribeFromErc20BalanceChanges message);
    }
}