namespace EthereumSamurai.EventProvider.Service.Actors.Proxies
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