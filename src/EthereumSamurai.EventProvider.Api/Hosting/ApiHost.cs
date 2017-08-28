namespace EthereumSamurai.EventProvider.Api.Hosting
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;

    public sealed class ApiHost : IApiHost
    {
        private readonly IWebHost _innerHost;

        public ApiHost(IWebHost webhost)
        {
            _innerHost = webhost;
        }

        public void Dispose()
        {
            _innerHost?.Dispose();
        }

        public void Start()
        {
            _innerHost.Start();
        }

        public async Task StopAsync()
        {
            await _innerHost.StopAsync();
        }
    }
}