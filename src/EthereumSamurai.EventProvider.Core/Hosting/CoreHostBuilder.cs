namespace EthereumSamurai.EventProvider.Core.Hosting
{
    using Api.Hosting;
    using Api.Hosting.Interfaces;
    using Autofac;
    using Interfaces;
    using Service.Hosting;
    using Service.Hosting.Interfaces;


    public class CoreHostBuilder : ICoreHostBuilder
    {
        public ICoreHost Build()
        {
            var containerBuilder = new ContainerBuilder();
            var startup          = new Startup();
            

            startup.ConfigureServices(containerBuilder);
            
            ConfigureApiHost(containerBuilder);

            ConfigureServiceHost(containerBuilder);

            ConfigureCoreHost(containerBuilder);


            var container = containerBuilder.Build();
            
            return container.Resolve<ICoreHost>();
        }

        private static void ConfigureApiHost(ContainerBuilder builder)
        {
            builder
                .RegisterType<ApiHostBuilder>()
                .As<IApiHostBuilder>()
                .SingleInstance();

            builder
                .Register((context, parameters) => context.Resolve<IApiHostBuilder>().Build());
        }

        private static void ConfigureCoreHost(ContainerBuilder builder)
        {
            builder
                .RegisterType<CoreHost>()
                .As<ICoreHost>()
                .SingleInstance();
        }

        private static void ConfigureServiceHost(ContainerBuilder builder)
        {
            builder
                .RegisterType<ServiceHostBuilder>()
                .As<IServiceHostBuilder>()
                .SingleInstance();

            builder
                .Register((context, parameters) => context.Resolve<IServiceHostBuilder>().Build());
        }
    }
}