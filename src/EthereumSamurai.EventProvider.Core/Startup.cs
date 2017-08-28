namespace EthereumSamurai.EventProvider.Core
{
    using System.Reflection;
    using Akka.Actor;
    using Akka.Configuration;
    using Autofac;


    public sealed class Startup
    {
        public void ConfigureServices(ContainerBuilder builder)
        {
            builder.RegisterInstance(ActorSystem.Create
            (
                name:   "EthereumSamuraiEventProvider",
                config: GetSystemConfig()
            ));
        }

        private static Config GetSystemConfig()
        {
            return ConfigurationFactory.FromResource
            (
                resourceName: "EthereumSamurai.EventProvider.Core.System.json",
                assembly:     Assembly.GetExecutingAssembly()
            );
        }
    }
}