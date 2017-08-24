namespace EthereumSamurai.EventProvider
{
    public interface IConfiguration
    {
        int NumberOfErc20TransfersObservers { get; }

        int NumberOfSubscribersNotifiers { get; }
    }
}