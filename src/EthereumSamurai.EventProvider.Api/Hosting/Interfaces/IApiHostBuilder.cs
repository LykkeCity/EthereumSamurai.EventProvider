namespace EthereumSamurai.EventProvider.Api.Hosting.Interfaces
{
    using Microsoft.AspNetCore.Hosting;

    public interface IApiHostBuilder
    {
        IWebHost Build();
    }
}