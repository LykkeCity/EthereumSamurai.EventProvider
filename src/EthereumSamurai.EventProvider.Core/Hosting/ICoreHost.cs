namespace EthereumSamurai.EventProvider.Core.Hosting
{
    using System;
    using System.Threading.Tasks;

    public interface ICoreHost : IDisposable
    {
        void Start();
        
        Task StopAsync();
    }
}