namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using Messages;
    using System;

    public interface IErc20TransferCommitsReplayManagerBehavior
    {
        void Process(ReplayErc20TransferCommits message, Action<Notify> sendNotificationAction);
    }
}