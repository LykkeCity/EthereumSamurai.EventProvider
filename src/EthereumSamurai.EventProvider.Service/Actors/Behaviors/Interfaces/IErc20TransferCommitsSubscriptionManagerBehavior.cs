namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors.Interfaces
{
    using System;
    using Messages;

    internal interface IErc20TransferCommitsSubscriptionManagerBehavior
    {
        void Process(SubscribeToErc20TransferCommits message, Action<ReplayErc20TransferCommits> requestReplayAction);

        void Process(UnsubscribeFromErc20TransferCommits message);
    }
}