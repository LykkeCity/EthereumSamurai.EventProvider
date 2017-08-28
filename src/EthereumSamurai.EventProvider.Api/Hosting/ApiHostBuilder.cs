namespace EthereumSamurai.EventProvider.Api.Hosting
{
    using Autofac;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;


    public sealed class ApiHostBuilder : IApiHostBuilder
    {
        private readonly ILifetimeScope _parentScope;


        public ApiHostBuilder(ILifetimeScope parentScope)
        {
            _parentScope = parentScope;
        }

        
        public IApiHost Build()
        {
            var startup        = new Startup(_parentScope);
            var webhostBuilder = WebHost.CreateDefaultBuilder();

            webhostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartup>(startup);
            });

            webhostBuilder.UseSetting("applicationName", "EthereumSamurai.EventProvider.Api");

            var webhost = webhostBuilder.Build();

            return new ApiHost(webhost);
        }
    }
}