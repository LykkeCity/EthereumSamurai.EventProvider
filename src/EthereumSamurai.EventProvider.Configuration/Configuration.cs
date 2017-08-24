namespace EthereumSamurai.EventProvider
{
    public class Configuration : IConfiguration
    {
        public int NumberOfErc20TransfersObservers { get; }
        public int NumberOfSubscribersNotifiers { get; }
    }
}