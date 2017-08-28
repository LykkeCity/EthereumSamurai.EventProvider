namespace EthereumSamurai.EventProvider.Api.Hosting
{
    using System;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;


    public abstract class ApiHostStartup : IStartup
    {
        private readonly ILifetimeScope _parentScope;


        protected ApiHostStartup(ILifetimeScope parentScope)
        {
            _parentScope = parentScope;
        }


        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var scope = _parentScope.BeginLifetimeScope(builder =>
            {
                builder.Populate(services);
            });

            return new AutofacServiceProvider(scope);
        }

        public abstract void Configure(IApplicationBuilder app);
    }
}