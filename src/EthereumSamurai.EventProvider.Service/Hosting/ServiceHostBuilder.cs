namespace EthereumSamurai.EventProvider.Service.Hosting
{
    using Akka.Actor;
    using Akka.DI.AutoFac;
    using Akka.DI.Core;
    using Autofac;


    public class ServiceHostBuilder : IServiceHostBuilder
    {
        private readonly ILifetimeScope _parentScope;
        private readonly ActorSystem    _system;


        public ServiceHostBuilder(
            ILifetimeScope parentScope,
            ActorSystem    system)
        {
            _parentScope = parentScope;
            _system      = system;
        }

        
        public IServiceHost Build()
        {
            var startup = new Startup();
            var scope   = _parentScope.BeginLifetimeScope(x =>
            {
                startup.ConfigureServices(x);

                x.RegisterType<ServiceHost>()
                 .As<IServiceHost>()
                 .SingleInstance();
            });

            var systemDependencyResolver = new AutoFacDependencyResolver(scope, _system);

            _system.AddDependencyResolver(systemDependencyResolver);

            return scope.Resolve<IServiceHost>();
        }
        
    }
}