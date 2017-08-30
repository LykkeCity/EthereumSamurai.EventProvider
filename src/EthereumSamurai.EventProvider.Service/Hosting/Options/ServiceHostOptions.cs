namespace EthereumSamurai.EventProvider.Service.Hosting.Options
{
    public class ServiceHostOptions
    {
        public ServiceHostOptions()
        {
            NumberOfErc20BalanceChangesReplayManagers        = 1;
            NumberOfErc20BalanceChangesSubscriptionManagers  = 1;
            NumberOfErc20TransferCommitsObservers            = 1;
            NumberOfErc20TransferCommitsReplayManagers       = 1;
            NumberOfErc20TransferCommitsSubscriptionManagers = 1;
            NumberOfSubscribersNotifiers                     = 1;
        }

        public int NumberOfErc20BalanceChangesReplayManagers { get; set; }

        public int NumberOfErc20BalanceChangesSubscriptionManagers { get; set; }

        public int NumberOfErc20TransferCommitsObservers { get; set; }

        public int NumberOfErc20TransferCommitsReplayManagers { get; set; }

        public int NumberOfErc20TransferCommitsSubscriptionManagers { get; set; }

        public int NumberOfSubscribersNotifiers { get; set; }

    }
}