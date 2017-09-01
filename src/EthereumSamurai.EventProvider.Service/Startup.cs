namespace EthereumSamurai.EventProvider.Service
{
    using Actors.Extensions;
    using Extensions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Repositories;


    public sealed class Startup
    {
        private readonly IConfigurationRoot _configuration;

        public Startup(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMemoryCache()
                .AddOptions()
                .AddRepositories()
                .AddActors();

            services
                .ConfigureOptions(_configuration);
        }
    }
}