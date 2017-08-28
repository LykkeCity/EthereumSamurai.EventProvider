using System;
using EthereumSamurai.EventProvider.Service.Messages;

namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using Messages;

    public interface IErc20TransferCommitsReplayManagerBehavior
    {
        void Process(ReplayErc20TransferCommits message, Action<Notify> sendNotificationAction);
    }
}