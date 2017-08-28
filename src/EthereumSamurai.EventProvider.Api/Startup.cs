namespace EthereumSamurai.EventProvider.Api
{
    using System;
    using Autofac;
    using Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;


    public sealed class Startup : ApiHostStartup
    {
        public Startup(ILifetimeScope parentScope)
            : base(parentScope)
        {

        }


        public override void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }

        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            return base.ConfigureServices(services);
        }
    }
}