namespace EthereumSamurai.EventProvider.Api
{
    using System;
    using Autofac;
    using Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Options;


    public sealed class Startup : ApiHostStartup
    {
        private readonly IConfigurationRoot _configuration;

        public Startup(
            IConfigurationRoot configuration,
            ILifetimeScope     parentScope) : base(parentScope)
        {
            _configuration = configuration;
        }


        public override void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }

        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddOptions();

            services.Configure<ApiOptions>(_configuration.GetSection("Api"));

            return base.ConfigureServices(services);
        }
    }
}