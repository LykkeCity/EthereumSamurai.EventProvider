using System;
using EthereumSamurai.EventProvider.Core.Messages;

namespace EthereumSamurai.EventProvider.Core.Actors.Behaviors
{
    public interface IErc20TransferCommitsReplayManagerBehavior
    {
        void Process(ReplayErc20TransferCommits message, Action<Notify> sendNotificationAction);
    }
}