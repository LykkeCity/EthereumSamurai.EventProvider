namespace EthereumSamurai.EventProvider.Api.Hosting
{
    using Autofac;
    using Interfaces;
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

        
        public IWebHost Build()
        {
            var startup        = new Startup(_configuration, _parentScope);
            var webhostBuilder = WebHost.CreateDefaultBuilder();

            webhostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartup>(startup);
            });

            webhostBuilder.UseSetting("applicationName", "EthereumSamurai.EventProvider.Api");

            var host = _configuration.GetValue<string>("Api:Host") ?? "http://*:5000";
            if (!string.IsNullOrEmpty(host))
            {
                webhostBuilder.UseUrls(host);
            }

            return webhostBuilder.Build();
        }
    }
}