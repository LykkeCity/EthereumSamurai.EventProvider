namespace EthereumSamurai.EventProvider.Service.Hosting.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IServiceHost : IDisposable
    {
        void Start();
        
        Task StopAsync();
    }
}