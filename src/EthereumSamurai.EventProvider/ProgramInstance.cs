namespace EthereumSamurai.EventProvider
{
    using System;
    using System.Runtime.Loader;
    using System.Threading;
    using Core.Hosting.Interfaces;


    internal sealed class ProgramInstance
    {
        private static readonly TimeSpan ShutdownTimeout = TimeSpan.FromSeconds(90);
        
        private readonly ICoreHostBuilder _coreHostBuilder;
        private readonly EventWaitHandle  _shutdownCompleted;
        private readonly EventWaitHandle  _shutdownStarted;

        private bool _exceptionCaught;
        private bool _signalProcessed;

        
        public ProgramInstance(ICoreHostBuilder coreHostBuilder)
        {
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
                    //// Process will not terminated in debug mode of Visual Studio,
                    //// but, if process started from console, it terminates successfully.

                    Console.WriteLine("Termination signal received. Application will be terminated.");
                }
            };
        }

        public async void Run()
        {
            try
            {
                using (var core = _coreHostBuilder.Build())
                {
                    core.Start();
                    
                    Console.WriteLine("Application has been started and is waiting for termination signal.");
                    
                    _shutdownStarted.WaitOne();
                    
                    await core.StopAsync();
                }

                Console.WriteLine("Application has been stopped.");

                _shutdownCompleted.Set();
            }
            catch (Exception e)
            {
                _exceptionCaught = true;

                Console.WriteLine(e);
                Console.WriteLine("Application will be terminated.");
            }
        }

        private void ShutdownGracefully()
        {
            _shutdownStarted.Set();

            Console.WriteLine("Termination signal received. Application will be stopped gracefully.");

            _shutdownCompleted.WaitOne(ShutdownTimeout);
        }
    }
}