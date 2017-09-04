namespace EthereumSamurai.EventProvider.Api
{
    using System;
    using System.IO;
    using Autofac;
    using Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.PlatformAbstractions;
    using Options;
    using Swashbuckle.AspNetCore.Swagger;


    internal sealed class Startup : ApiHostStartup
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

            app.UseSwagger();
            
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/api/swagger.json", "Ethereum Samurai Event Provider API");
            });
        }

        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc();

            services
                .AddOptions();

            services
                .AddSwaggerGen(opt =>
                {
                    opt.SwaggerDoc
                    (
                        name: "api",
                        info: new Info()
                    );

                    var basePath   = PlatformServices.Default.Application.ApplicationBasePath;
                    var xmlDocPath = Path.Combine(basePath, "EthereumSamurai.EventProvider.Api.xml");

                    opt.IncludeXmlComments(xmlDocPath);
                });

            services
                .Configure<ApiOptions>(_configuration.GetSection("Api"));

            return base.ConfigureServices(services);
        }
    }
}