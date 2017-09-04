namespace EthereumSamurai.EventProvider
{
    using Core.Hosting;
    

    internal sealed class Program
    {
        private static void Main()
        {
            BuildInstance().Run();
        }


        private static ProgramInstance BuildInstance()
        {
            return new ProgramInstance
            (
                new CoreHostBuilder()
            );
        }
    }
}