namespace EthereumSamurai.EventProvider.Core.Actors.Proxies
{
    using Akka.Actor;

    public class Erc20TransferCommitsReplayManagerProxy : ActorProxy, IErc20TransferCommitsReplayManagerProxy
    {
        public Erc20TransferCommitsReplayManagerProxy(IActorRefFactory actorSystem) 
            : base(actorSystem, ActorPaths.Erc20TransferCommitsReplayManager)
        {
        }
    }
}