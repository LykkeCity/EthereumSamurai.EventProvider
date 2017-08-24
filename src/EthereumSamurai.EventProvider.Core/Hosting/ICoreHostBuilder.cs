namespace EthereumSamurai.EventProvider.Core.Hosting
{
    using Microsoft.Extensions.DependencyInjection;

    public interface ICoreHostBuilder
    {
        ICoreHost Build();
    }
}