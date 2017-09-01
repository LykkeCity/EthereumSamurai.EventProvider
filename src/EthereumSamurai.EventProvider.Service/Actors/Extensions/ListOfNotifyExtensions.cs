namespace EthereumSamurai.EventProvider.Service.Actors.Extensions
{
    using System;
    using System.Collections.Generic;
    using Messages;
    using Notifications.Interfaces;



    internal static class QueueOfNotifyExtensions
    {
        public static void EnqueueUnicast(this Queue<Notify> notifications, INotification notification, string exchange, string routingKey)
        {
            notifications.Enqueue
            (
                new Notify
                (
                    exchange:     exchange,
                    routingKey:   routingKey,
                    notification: notification
                )
            );
        }

        public static void EnqueueMulticastRange(this Queue<Notify> notifications, INotification notification, IEnumerable<(string exchange, string routingKey)> subscribers)
        {
            foreach (var subscriber in subscribers)
            {
                notifications.EnqueueUnicast(notification, subscriber.exchange, subscriber.routingKey);
            }
        }

        public static void ForEachDequeue(this Queue<Notify> notifications, Action<Notify> action)
        {
            while (notifications.Count > 0)
            {
                action(notifications.Dequeue());
            }
        }
    }
}