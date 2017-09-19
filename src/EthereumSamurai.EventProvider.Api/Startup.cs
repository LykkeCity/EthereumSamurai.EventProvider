namespace EthereumSamurai.EventProvider.Api
{
    using System;
    using System.IO;
    using System.Reflection;
    using Autofac;
    using Filters;
    using Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.PlatformAbstractions;
    using Options;
    using Service.Actors.Proxies;
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
                .AddMvc()
                .AddMvcOptions(x =>
                {
                    x.Filters.Insert(0, new ValidateModelFilter());
                });

            services
                .AddOptions();

            services
                .AddSwaggerGen(opt =>
                {
                    opt.SwaggerDoc
                    (
                        name: "api",
                        info: new Info
                        {
                            Version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion
                        }
                    );
                    
                    var basePath   = PlatformServices.Default.Application.ApplicationBasePath;
                    var xmlDocPath = Path.Combine(basePath, "EthereumSamurai.EventProvider.Api.xml");

                    opt.IncludeXmlComments(xmlDocPath);
                });

            services
                .AddSingleton<IErc20BalanceChangesReplayManagerProxy, Erc20BalanceChangesReplayManagerProxy>()
                .AddSingleton<IErc20BalanceChangesSubscribtionManagerProxy, Erc20BalanceChangesSubscribtionManagerProxy>()
                .AddSingleton<IErc20TransferCommitsReplayManagerProxy, Erc20TransferCommitsReplayManagerProxy>()
                .AddSingleton<IErc20TransferCommitsSubscriptionManagerProxy, Erc20TransferCommitsSubscriptionManagerProxy>();

            services
                .Configure<ApiOptions>(_configuration.GetSection("Api"));

            return base.ConfigureServices(services);
        }
    }
}