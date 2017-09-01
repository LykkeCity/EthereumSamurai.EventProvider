namespace EthereumSamurai.EventProvider.Core.Hosting
{
    using System;
    using System.Threading.Tasks;
    using Interfaces;
    using Microsoft.AspNetCore.Hosting;
    using Service.Hosting.Interfaces;


    public class CoreHost : ICoreHost
    {
        private readonly IWebHost     _apiHost;
        private readonly IServiceHost _serviceHost;


        public CoreHost(
            IWebHost     apiHost,
            IServiceHost serviceHost)
        {
            _apiHost     = apiHost     ?? throw new ArgumentNullException(nameof(apiHost));
            _serviceHost = serviceHost ?? throw new ArgumentNullException(nameof(serviceHost));
        }


        public void Dispose()
        {
            _apiHost.Dispose();
            _serviceHost.Dispose();
        }
        
        public void Start()
        {
            _serviceHost.Start();
            _apiHost.Start();
        }

        public async Task StopAsync()
        {
            await _apiHost.StopAsync();
            await _serviceHost.StopAsync();
        }
    }
}