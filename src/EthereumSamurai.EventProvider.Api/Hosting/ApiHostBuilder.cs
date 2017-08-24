namespace EthereumSamurai.EventProvider.Api.Hosting
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;



    public sealed class ApiHostBuilder : IApiHostBuilder
    {
        private readonly IWebHostBuilder _innerBuilder;



        public ApiHostBuilder()
        {
            _innerBuilder = WebHost
                .CreateDefaultBuilder()
                .UseStartup<Startup>();
        }



        public IWebHost Build()
            => _innerBuilder.Build();
    }
}