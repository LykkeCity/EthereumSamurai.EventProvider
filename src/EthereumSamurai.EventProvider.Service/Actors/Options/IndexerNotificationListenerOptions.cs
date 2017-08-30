namespace EthereumSamurai.EventProvider.Service.Actors.Options
{
    public class IndexerNotificationListenerOptions
    {
        public IndexerNotificationListenerOptions()
        {
            NotificationsQueue = "EthereumSamuraiNotifications";
        }

        public string NotificationsQueue { get; set; }
    }
}