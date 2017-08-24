namespace EthereumSamurai.EventProvider.Core.Hosting
{
    using System.Threading.Tasks;
    using Actors;
    using Akka.Actor;
    using Akka.DI.Core;
    using Akka.Routing;

    internal class CoreHost : ICoreHost
    {
        private readonly IConfiguration _configuration;
        private readonly ActorSystem    _system;

        public CoreHost(
            IConfiguration configuration,
            ActorSystem    system)
        {
            _system = system;
        }


        private void CreateActor<T>(ActorMetadata metadata, RouterConfig routerConfig = null)
            where T : ActorBase
        {
            var props = _system.DI().Props<T>();

            if (routerConfig != null)
            {
                props = props.WithRouter(routerConfig);
            }

            _system.ActorOf(props, metadata.Name);
        }

        public void Dispose()
        {
            _system?.Dispose();
        }

        public void Start()
        {
            CreateActor<Erc20BalanceChangesObserverActor>
            (
                metadata: ActorPaths.Erc20BalanceChangesObserver
            );

            CreateActor<Erc20TransferCommitsObserverActor>
            (
                metadata:     ActorPaths.Erc20TransferCommitsObserver,
                routerConfig: new SmallestMailboxPool(_configuration.NumberOfErc20TransfersObservers)
            );

            CreateActor<IndexerNotificationsListenerActor>
            (
                metadata: ActorPaths.IndexerNotificationsListener
            );

            CreateActor<SubscribersNotifierActor>
            (
                metadata:     ActorPaths.SubscribersNotifier,
                routerConfig: new ConsistentHashingPool(_configuration.NumberOfSubscribersNotifiers)
            );
        }

        public async Task StopAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}