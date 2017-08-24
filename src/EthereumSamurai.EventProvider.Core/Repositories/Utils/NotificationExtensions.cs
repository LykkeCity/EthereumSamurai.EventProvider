namespace EthereumSamurai.EventProvider.Core.Repositories.Utils
{
    using System.Collections.Generic;
    using Messages;
    using Notifications;

    internal static class NotificationExtensions
    {
        public static Notify Envelope(this INotification notification, string exchange, string routingKey)
        {
            return new Notify
            (
                exchange:     exchange,
                routingKey:   routingKey,
                notification: notification
            );
        }

        public static void Add(this IList<Notify> notifications, INotification notification, string exchange, string routingKey)
        {
            notifications.Add
            (
                notification.Envelope(exchange, routingKey)
            );
        }

        public static void AddRange(this IList<Notify> notifications, INotification notification, IEnumerable<(string exchange, string routingKey)> subscribers)
        {
            foreach (var subscriber in subscribers)
            {
                notifications.Add(notification, subscriber.exchange, subscriber.routingKey);
            }
        }
    }
}