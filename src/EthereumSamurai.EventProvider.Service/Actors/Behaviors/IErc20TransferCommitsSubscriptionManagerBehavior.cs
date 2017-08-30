namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using System;
    using Messages;

    public interface IErc20TransferCommitsSubscriptionManagerBehavior
    {
        void Process(SubscribeToErc20TransferCommits message, Action<ReplayErc20TransferCommits> requestReplayAction);

        void Process(UnsubscribeFromErc20TransferCommits message);
    }
}