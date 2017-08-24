namespace EthereumSamurai.EventProvider.Core.DependencyInjection
{
    using System;
    using System.Collections.Concurrent;
    using Akka.Actor;
    using Akka.DI.Core;
    


    public class DependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider                   _container;
        private readonly ActorSystem                        _system;
        private readonly ConcurrentDictionary<string, Type> _typeCache;


        public DependencyResolver(IServiceProvider container, ActorSystem system)
        {
            _container = container;
            _system    = system;
            _typeCache = new ConcurrentDictionary<string, Type>();
        }



        public Type GetType(string actorName)
        {
            return _typeCache.GetOrAdd(actorName, key => key.GetTypeValue());
        }

        public Func<ActorBase> CreateActorFactory(Type actorType)
        {
            return () => (ActorBase) _container.GetService(actorType);
        }

        public Props Create<TActor>() where TActor : ActorBase
        {
            return Create(typeof(TActor));
        }

        public Props Create(Type actorType)
        {
            return _system.GetExtension<DIExt>().Props(actorType);
        }

        public void Release(ActorBase actor)
        {
            //// Can not be implemented with Microsoft's IServiceProvider
        }
    }
}