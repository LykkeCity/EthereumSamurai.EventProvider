namespace EthereumSamurai.EventProvider
{
    public class Configuration : IConfiguration
    {
        public string IndexerNotificationsQueue { get; }
        public int NumberOfErc20TransfersObservers { get; }
        public int NumberOfSubscribersNotifiers { get; }
    }
}