namespace EthereumSamurai.EventProvider.Service.Actors.Options
{
    public class IndexerNotificationListenerOptions
    {
        public IndexerNotificationListenerOptions()
        {
            NotificationsExchange = "lykke.ethereum.indexer";
            NotificationsQueue    = "event-provider-indexer-notificationss";
        }

        public string NotificationsExchange { get; set; }

        public string NotificationsQueue { get; set; }
    }
}