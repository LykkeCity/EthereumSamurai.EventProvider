namespace EthereumSamurai.EventProvider.Service.Hosting
{
    using System.Threading.Tasks;
    using Actors;
    using Akka.Actor;
    using Akka.DI.Core;
    using Akka.Routing;
    using Interfaces;
    using Microsoft.Extensions.Options;
    using Options;


    internal class ServiceHost : IServiceHost
    {
        private readonly ServiceHostOptions _options;
        private readonly ActorSystem        _system;


        public ServiceHost(
            IOptions<ServiceHostOptions> options, 
            ActorSystem                  system)
        {
            _options = options.Value;
            _system  = system;
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

        }

        private void StartErc20BalancesSubsystem()
        {
            CreateActor<Erc20BalanceChangesObserverActor>
            (
                metadata: ActorPaths.Erc20BalanceChangesObserver
            );

            CreateActor<Erc20BalanceChangesReplayManagerActor>
            (
                metadata:     ActorPaths.Erc20BalanceChangesReplayManager,
                routerConfig: new SmallestMailboxPool(_options.NumberOfErc20TransferCommitsReplayManagers)
            );

            CreateActor<Erc20BalanceChangesSubscribtionManagerActor>
            (
                metadata:     ActorPaths.Erc20BalanceChangesSubscriptionManager,
                routerConfig: new SmallestMailboxPool(_options.NumberOfErc20BalanceChangesSubscriptionManagers)
            );
        }

        private void StartErc20TransfersSubsystem()
        {
            CreateActor<Erc20TransferCommitsObserverActor>
            (
                metadata:     ActorPaths.Erc20TransferCommitsObserver,
                routerConfig: new SmallestMailboxPool(_options.NumberOfErc20TransferCommitsObservers)
            );

            CreateActor<Erc20TransferCommitsReplayManagerActor>
            (
                metadata:     ActorPaths.Erc20TransferCommitsReplayManager,
                routerConfig: new SmallestMailboxPool(_options.NumberOfErc20BalanceChangesReplayManagers)
            );

            CreateActor<Erc20BalanceChangesSubscribtionManagerActor>
            (
                metadata:     ActorPaths.Erc20TransferCommitsSubscriptionManager,
                routerConfig: new SmallestMailboxPool(_options.NumberOfErc20TransferCommitsSubscriptionManagers)
            );
        }

        private void StartSubscriberNotifiers()
        {
            CreateActor<SubscribersNotifierActor>
            (
                metadata:     ActorPaths.SubscribersNotifier,
                routerConfig: new ConsistentHashingPool(_options.NumberOfSubscribersNotifiers)
            );
        }
        
        private void StartIndexerNotificationsListener()
        {
            CreateActor<IndexerNotificationsListenerActor>
            (
                metadata: ActorPaths.IndexerNotificationsListener
            );
        }
    }
}