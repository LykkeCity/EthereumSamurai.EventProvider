namespace EthereumSamurai.EventProvider.Service.Repositories.Options
{
    public class Erc20SubscriptionRepositoryOptions
    {
        public Erc20SubscriptionRepositoryOptions()
        {
            CacheDuration = 900;
            UseCache      = true;
        }

        /// <summary>
        ///    Cache duration in seconds.
        /// </summary>
        public int CacheDuration { get; set; }
        
        public bool UseCache { get; set; }
    }
}