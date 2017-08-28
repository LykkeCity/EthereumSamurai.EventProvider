namespace EthereumSamurai.EventProvider.Notifications
{
    public interface INotification
    {
        /// <summary>
        ///    Type of the notification.
        /// </summary>
        string Type { get; }
    }
}