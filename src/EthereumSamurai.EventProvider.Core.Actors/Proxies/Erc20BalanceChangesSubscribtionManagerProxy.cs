namespace EthereumSamurai.EventProvider.Core.Actors.Proxies
{
    using Akka.Actor;

    public class Erc20BalanceChangesSubscribtionManagerProxy : ActorProxy, IErc20BalanceChangesSubscribtionManagerProxy
    {
        public Erc20BalanceChangesSubscribtionManagerProxy(IActorRefFactory actorSystem) 
            : base(actorSystem, ActorPaths.Erc20BalanceChangesSubscriptionManager)
        {

        }
    }
}