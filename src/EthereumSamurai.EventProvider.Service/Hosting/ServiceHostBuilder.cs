namespace EthereumSamurai.EventProvider.Service.Hosting
{
    using Akka.Actor;
    using Akka.DI.AutoFac;
    using Akka.DI.Core;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;


    public class ServiceHostBuilder : IServiceHostBuilder
    {
        private readonly IConfigurationRoot _configuration;
        private readonly ILifetimeScope     _parentScope;
        private readonly ActorSystem        _system;


        public ServiceHostBuilder(
            IConfigurationRoot configuration,
            ILifetimeScope     parentScope,
            ActorSystem        system)
        {
            _configuration = configuration;
            _parentScope   = parentScope;
            _system        = system;
        }

        
        public IServiceHost Build()
        {
            var startup = new Startup(_configuration);
            var scope   = _parentScope.BeginLifetimeScope(builder =>
            {
                var services = new ServiceCollection();
                
                startup.ConfigureServices(services);

                builder.Populate(services);
                
                builder.RegisterType<ServiceHost>()
                 .As<IServiceHost>()
                 .SingleInstance();
            });
            

            var systemDependencyResolver = new AutoFacDependencyResolver(scope, _system);

            _system.AddDependencyResolver(systemDependencyResolver);

            return scope.Resolve<IServiceHost>();
        }
        
    }
}