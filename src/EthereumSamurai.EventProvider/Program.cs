namespace EthereumSamurai.EventProvider
{
    using Api.Hosting;
    using Core.Hosting;
    using Microsoft.Extensions.DependencyInjection;


    internal class Program
    {
        private static void Main()
        {
            BuildInstance().Run();
        }

        private static ProgramInstance BuildInstance()
        {
            var services        = new ServiceCollection();
            var apiHostBuilder  = new ApiHostBuilder(services);
            var coreHostBuilder = new CoreHostBuilder(services);

            return new ProgramInstance(apiHostBuilder, coreHostBuilder);
        }
    }
}