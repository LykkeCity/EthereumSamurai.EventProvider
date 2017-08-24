namespace EthereumSamurai.EventProvider.Api.Hosting
{
    using Microsoft.AspNetCore.Hosting;

    public interface IApiHostBuilder
    {
        IWebHost Build();
    }
}