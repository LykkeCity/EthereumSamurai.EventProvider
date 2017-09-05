namespace EthereumSamurai.EventProvider.Core
{
    using System.Reflection;
    using Akka.Actor;
    using Akka.Configuration;
    using Autofac;
    using Extensions;
    using Microsoft.Extensions.Configuration;


    internal sealed class Startup
    {
        public void ConfigureServices(ContainerBuilder builder)
        {
            var actorSystem = ActorSystem.Create
            (
                name:   "EthereumSamuraiEventProvider",
                config: GetActorSystemConfig()
            );

            builder
                .RegisterInstance(actorSystem);

            builder
                .RegisterInstance(actorSystem)
                .As<IActorRefFactory>();

            builder
                .RegisterInstance
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
        /// <remarks>
        ///    In most cases you will never change these settings after build.
        /// </remarks>
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
                // Never place sensitive information in this file (in fact, it should only contain template with defaults)!
                .AddJsonFile("configuration.json", optional: true)
                // You should place sensitive information for development purposes here (this file is .gitignored).
                .AddJsonFile("configuration.development.json", optional: true)
                // You should place sensitive information for production purposes here...
                .AddEnvironmentVariables()
                // ...and here.
                .AddLykkeSettings("LykkeSettings");

            return builder.Build();
        }
    }
}