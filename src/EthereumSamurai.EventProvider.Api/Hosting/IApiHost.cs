namespace EthereumSamurai.EventProvider.Api.Hosting
{
    using System;
    using System.Threading.Tasks;

    public interface IApiHost : IDisposable
    {
        void Start();

        Task StopAsync();
    }
}