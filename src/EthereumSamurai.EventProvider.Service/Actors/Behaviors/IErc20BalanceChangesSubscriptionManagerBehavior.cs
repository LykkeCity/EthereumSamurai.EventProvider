namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using System;
    using Messages;

    public interface IErc20BalanceChangesSubscriptionManagerBehavior
    {
        void Process(SubscribeToErc20BalanceChanges message, Action<ReplayErc20BalanceChanges> requestReplayAction);

        void Process(UnsubscribeFromErc20BalanceChanges message);
    }
}