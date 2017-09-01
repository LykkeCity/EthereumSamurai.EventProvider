namespace EthereumSamurai.EventProvider.Notifications.Interfaces
{
    public interface INotification
    {
        /// <summary>
        ///    Type of the notification.
        /// </summary>
        string Type { get; }
    }
}