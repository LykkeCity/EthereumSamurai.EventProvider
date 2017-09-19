namespace EthereumSamurai.EventProvider.Api.Hosting.Interfaces
{
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    ///    A builder for the <see cref="IWebHost"/>.
    /// </summary>
    public interface IApiHostBuilder
    {
        /// <summary>
        ///    Builds an <see cref="IWebHost" /> which hosts a web application.
        /// </summary>
        IWebHost Build();
    }
}