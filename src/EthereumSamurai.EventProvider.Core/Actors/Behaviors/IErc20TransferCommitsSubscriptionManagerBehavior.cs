using System;
using EthereumSamurai.EventProvider.Core.Messages;

namespace EthereumSamurai.EventProvider.Core.Actors.Behaviors
{
    public interface IErc20TransferCommitsSubscriptionManagerBehavior
    {
        void Process(SubscribeToErc20TransferCommits message, Action<ReplayErc20TransferCommits> requestReplayAction);
        void Process(UnsubscribeFromErc20TransferCommits message);
    }
}