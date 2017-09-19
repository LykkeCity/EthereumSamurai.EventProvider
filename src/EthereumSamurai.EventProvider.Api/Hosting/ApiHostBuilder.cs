namespace EthereumSamurai.EventProvider.Api.Hosting
{
    using Autofac;
    using Interfaces;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;


    /// <inheritdoc />
    public sealed class ApiHostBuilder : IApiHostBuilder
    {
        private readonly IConfigurationRoot _configuration;
        private readonly ILifetimeScope     _parentScope;

        /// <summary>
        ///    Initializes a new instance of the <see cref="ApiHostBuilder"/> class.
        /// </summary>
        public ApiHostBuilder(
            IConfigurationRoot configuration,
            ILifetimeScope     parentScope)
        {
            _configuration = configuration;
            _parentScope   = parentScope;
        }

        /// <inheritdoc />
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