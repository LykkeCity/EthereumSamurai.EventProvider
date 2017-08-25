namespace EthereumSamurai.EventProvider
{
    using System;
    using System.Runtime.Loader;
    using System.Threading;
    using Api.Hosting;
    using Core.Hosting;



    internal class ProgramInstance
    {
        private static readonly TimeSpan ShutdownTimeout = TimeSpan.FromSeconds(90);

        private readonly IApiHostBuilder     _apiHostBuilder;
        private readonly ICoreHostBuilder    _coreHostBuilder;
        private readonly EventWaitHandle     _shutdownCompleted;
        private readonly EventWaitHandle     _shutdownStarted;

        private bool _exceptionCaught;
        private bool _signalProcessed;



        public ProgramInstance(
            IApiHostBuilder apiHostBuilder,
            ICoreHostBuilder coreHostBuilder)
        {
            _apiHostBuilder    = apiHostBuilder;
            _coreHostBuilder   = coreHostBuilder;
            _shutdownCompleted = new ManualResetEvent(false);
            _shutdownStarted   = new ManualResetEvent(false);
            
            RegisterSignalHandlers();
        }
        
        private void RegisterSignalHandlers()
        {
            AssemblyLoadContext.Default.Unloading += ctx =>
            {
                if (!_signalProcessed && !_exceptionCaught)
                {
                    // Procesing SIGTERM (or another received and unpocessed signal)
                    ShutdownGracefully();
                }
            };

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                _signalProcessed = true;

                //// Processing Ctrl-C or SIGINT 
                if (eventArgs.SpecialKey == ConsoleSpecialKey.ControlC)
                {
                    ShutdownGracefully();

                    eventArgs.Cancel = true;
                }
                //// Processing Ctrl-Break or SIGQUIT
                else
                {
                    //// Process will not terminate in debug mode of Visual Studio,
                    //// but, if process started from console, it terminates successfully.

                    Console.WriteLine("Termination signal received. Service will be terminated.");
                }
            };
        }

        public async void Run()
        {
            try
            {
                using (var core = _coreHostBuilder.Build())
                using (var api  =  _apiHostBuilder.Build())
                {
                    core.Start();
                    api.Start();

                    Console.WriteLine("Service has been started and is waiting for termination signal.");
                    
                    _shutdownStarted.WaitOne();

                    await api.StopAsync();
                    await core.StopAsync();
                }

                Console.WriteLine("Service has been stopped.");

                _shutdownCompleted.Set();
            }
            catch (Exception e)
            {
                _exceptionCaught = true;

                Console.WriteLine(e);
                Console.WriteLine("Service will be terminated.");
            }
        }

        private void ShutdownGracefully()
        {
            _shutdownStarted.Set();

            Console.WriteLine("Termination signal received. Service will be stopped gracefully.");

            _shutdownCompleted.WaitOne(ShutdownTimeout);
        }
    }
}