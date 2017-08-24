namespace EthereumSamurai.EventProvider.Core.Actors.Proxies
{
    using Akka.Actor;

    public class Erc20BalanceChangesReplayManagerProxy : ActorProxy, IErc20BalanceChangesReplayManagerProxy
    {
        public Erc20BalanceChangesReplayManagerProxy(IActorRefFactory actorSystem) 
            : base(actorSystem, ActorPaths.Erc20BalanceChangesReplayManager)
        {

        }
    }
}