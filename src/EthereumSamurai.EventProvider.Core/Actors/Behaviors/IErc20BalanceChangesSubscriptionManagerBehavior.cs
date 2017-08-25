using System;
using EthereumSamurai.EventProvider.Core.Messages;

namespace EthereumSamurai.EventProvider.Core.Actors.Behaviors
{
    public interface IErc20BalanceChangesSubscriptionManagerBehavior
    {
        void Process(SubscribeToErc20BalanceChanges message, Action<ReplayErc20BalanceChanges> requestReplayAction);

        void Process(UnsubscribeFromErc20BalanceChanges message);
    }
}