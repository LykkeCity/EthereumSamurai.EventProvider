namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors.Interfaces
{
    using System;
    using Messages;

    internal interface IErc20TransferCommitsReplayManagerBehavior
    {
        void Process(ReplayErc20TransferCommits message, Action<Notify> sendNotificationAction);
    }
}