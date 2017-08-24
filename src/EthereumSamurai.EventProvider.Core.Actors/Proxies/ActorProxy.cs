namespace EthereumSamurai.EventProvider.Core.Actors.Proxies
{
    using System;
    using System.Threading.Tasks;
    using Akka.Actor;

    public abstract class ActorProxy : IActorProxy
    {
        private readonly ICanTell _actor;

        protected ActorProxy(IActorRefFactory actorSystem, ActorMetadata metadata)
        {
            _actor = actorSystem.ActorSelection(metadata.Path);
        }

        public async Task<object> Ask(object message, TimeSpan timeout)
        {
            return await _actor.Ask(message, timeout);
        }

        public async Task<T> Ask<T>(object message, TimeSpan timeout)
        {
            return await _actor.Ask<T>(message, timeout);
        }

        public void Tell(object message)
        {
            _actor.Tell(message, ActorRefs.Nobody);
            
        }
    }
}