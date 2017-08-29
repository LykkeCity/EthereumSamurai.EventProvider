namespace EthereumSamurai.EventProvider.Service.Options
{
    public class ServiceHostOptions
    {
        public ServiceHostOptions()
        {
            NumberOfErc20TransfersObservers = 1;
            NumberOfSubscribersNotifiers    = 1;
        }

        public int NumberOfErc20TransfersObservers { get; set; }

        public int NumberOfSubscribersNotifiers { get; set; }

    }
}