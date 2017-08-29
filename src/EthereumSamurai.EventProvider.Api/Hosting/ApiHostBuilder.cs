namespace EthereumSamurai.EventProvider.Api.Hosting
{
    using Autofac;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;


    public sealed class ApiHostBuilder : IApiHostBuilder
    {
        private readonly IConfigurationRoot _configuration;
        private readonly ILifetimeScope     _parentScope;


        public ApiHostBuilder(
            IConfigurationRoot configuration,
            ILifetimeScope     parentScope)
        {
            _configuration = configuration;
            _parentScope   = parentScope;
        }

        
        public IApiHost Build()
        {
            var startup        = new Startup(_configuration, _parentScope);
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