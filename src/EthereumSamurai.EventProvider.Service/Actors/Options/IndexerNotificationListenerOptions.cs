namespace EthereumSamurai.EventProvider.Service.Actors.Options
{
    public class IndexerNotificationListenerOptions
    {
        public IndexerNotificationListenerOptions()
        {
            NotificationsExchange = "lykke.ethereum.indexer";
            NotificationsQueue    = "lykke-ethereum-event-provider-indexer-notifications";
        }

        public string NotificationsExchange { get; set; }

        public string NotificationsQueue { get; set; }
    }
}