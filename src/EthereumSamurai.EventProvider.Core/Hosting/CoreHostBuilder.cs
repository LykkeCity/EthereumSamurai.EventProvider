namespace EthereumSamurai.EventProvider.Core.Hosting
{
    using System.Reflection;
    using Akka.Actor;
    using Akka.Configuration;
    using Akka.DI.Core;
    using DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;



    public class CoreHostBuilder : ICoreHostBuilder
    {
        private readonly IServiceCollection _services;
        private readonly Startup            _startup;


        public CoreHostBuilder()
        {
            _services = new ServiceCollection();
            _startup  = new Startup();
            
            _startup.ConfigureServices(_services);
        }



        public ICoreHost Build()
        {
            var serviceProviderFactory = new DefaultServiceProviderFactory();
            var containerBuilder       = serviceProviderFactory.CreateBuilder(_services);
            var container              = containerBuilder.BuildServiceProvider();

            var system = ActorSystem.Create
            (
                name:   "ethereum-samurai-event-provider",
                config: GetSystemConfig()
            );

            var diResolver = new DependencyResolver
            (
                container: container,
                system:    system
            );

            system.AddDependencyResolver(diResolver);

            return new CoreHost(null, system);
        }
        
        private static Config GetSystemConfig()
        {
            return ConfigurationFactory.FromResource
            (
                "EthereumSamurai.EventProvider.Core.system.json",
                Assembly.GetExecutingAssembly()
            );
        }
    }
}