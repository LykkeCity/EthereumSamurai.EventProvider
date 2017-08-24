namespace EthereumSamurai.EventProvider.Core.Actors.Proxies
{
    using Akka.Actor;

    public sealed class Erc20TransferCommitsSubscriptionManagerProxy : ActorProxy, IErc20TransferCommitsSubscriptionManagerProxy
    {
        public Erc20TransferCommitsSubscriptionManagerProxy(IActorRefFactory actorSystem) 
            : base(actorSystem, ActorPaths.Erc20TransferCommitsSubscriptionManager)
        {
        }
    }
}