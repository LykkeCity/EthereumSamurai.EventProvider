namespace EthereumSamurai.EventProvider.Service.Hosting
{
    using System.Threading.Tasks;
    using Actors;
    using Akka.Actor;
    using Akka.DI.Core;
    using Akka.Routing;


    internal class ServiceHost : IServiceHost
    {
        private readonly IConfiguration _configuration;
        private readonly ActorSystem    _system;


        public ServiceHost(
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

        }

        public void Start()
        {
            StartSubscriberNotifiers();

            StartErc20BalancesSubsystem();

            StartErc20TransfersSubsystem();

            StartIndexerNotificationsListener();
        }

        public async Task StopAsync()
        {
            throw new System.NotImplementedException();
        }

        private void StartErc20BalancesSubsystem()
        {
            CreateActor<Erc20BalanceChangesObserverActor>
            (
                metadata: ActorPaths.Erc20BalanceChangesObserver
            );
        }

        private void StartErc20TransfersSubsystem()
        {
            CreateActor<IndexerNotificationsListenerActor>
            (
                metadata: ActorPaths.IndexerNotificationsListener
            );
        }

        private void StartSubscriberNotifiers()
        {
            CreateActor<SubscribersNotifierActor>
            (
                metadata:     ActorPaths.SubscribersNotifier,
                routerConfig: new ConsistentHashingPool(_configuration.NumberOfSubscribersNotifiers)
            );
        }

        private void StartIndexerNotificationsListener()
        {
            CreateActor<Erc20TransferCommitsObserverActor>
            (
                metadata:     ActorPaths.Erc20TransferCommitsObserver,
                routerConfig: new SmallestMailboxPool(_configuration.NumberOfErc20TransfersObservers)
            );
        }
    }
}