namespace EthereumSamurai.EventProvider.Core
{
    using System.Reflection;
    using Akka.Actor;
    using Akka.Configuration;
    using Autofac;
    using Configuration;
    using Microsoft.Extensions.Configuration;


    public sealed class Startup
    {
        public void ConfigureServices(ContainerBuilder builder)
        {
            builder.RegisterInstance(ActorSystem.Create
            (
                name:   "EthereumSamuraiEventProvider",
                config: GetActorSystemConfig()
            ));

            builder.RegisterInstance
            (
                GetApplicationConfig()
            );
        }

        /// <summary>
        ///    Loads the akka.net actor system configuration from the embedded resource.
        /// </summary>
        /// <returns>
        ///    Instance of the <see cref="Config"/> class.
        /// </returns>
        private static Config GetActorSystemConfig()
        {
            return ConfigurationFactory.FromResource
            (
                resourceName: "EthereumSamurai.EventProvider.Core.System.json",
                assembly:     Assembly.GetExecutingAssembly()
            );
        }

        private static IConfigurationRoot GetApplicationConfig()
        {
            var builder = new ConfigurationBuilder();

            builder
                .AddJsonFile("configuration.json",             optional: false)
                .AddJsonFile("configuration.staging.json",     optional: true)
                .AddJsonFile("configuration.development.json", optional: true)
                .AddEnvironmentVariables()
                .AddLykkeSettings("LykkeSettings");

            return builder.Build();
        }
    }
}