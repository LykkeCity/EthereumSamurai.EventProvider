namespace EthereumSamurai.EventProvider.Api.Hosting
{
    using Autofac;
    using Microsoft.AspNetCore.Hosting;

    public interface IApiHostBuilder
    {
        IApiHost Build();
    }
}