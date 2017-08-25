namespace EthereumSamurai.EventProvider
{
    public interface IConfiguration
    {
        string IndexerNotificationsQueue { get; }

        int NumberOfErc20TransfersObservers { get; }

        int NumberOfSubscribersNotifiers { get; }
    }
}