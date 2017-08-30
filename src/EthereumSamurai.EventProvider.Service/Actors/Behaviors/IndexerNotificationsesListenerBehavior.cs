namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using System;
    using System.Linq;
    using System.Text;
    using Messages;
    using Newtonsoft.Json.Linq;
    using Options;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;


    public class IndexerNotificationsesListenerBehavior : IIndexerNotificationsListenerBehavior
    {
        private const string ConsumerTag = "IndexerNotificationsListener";

        private readonly IModel                             _channel;
        private readonly IndexerNotificationListenerOptions _options;


        public IndexerNotificationsesListenerBehavior(
            IModel                             channel,
            IndexerNotificationListenerOptions options)
        {
            _channel = channel;
            _options = options;
        }


        public void Process(
            IndexerNotificationReceived message,
            Action<BlockBalancesIndexed> notifyAboutIndexedBlockBalancesAction,
            Action<BlockIndexed> notifyAboutIndexedBlockAction)
        {
            var notificationJson = Encoding.UTF8.GetString(message.NotificationBody.ToArray());
            var notification     = JObject.Parse(notificationJson);

            if (notification.TryGetValue("$type", out var typeToken))
            {
                var type = typeToken.Value<string>();
                
                switch (type.ToLowerInvariant())
                {
                    case "blockbalancesindexed":
                        notifyAboutIndexedBlockBalancesAction
                        (
                            notification.ToObject<BlockBalancesIndexed>()
                        );
                        return;
                    case "blockindexed":
                        notifyAboutIndexedBlockAction
                        (
                            notification.ToObject<BlockIndexed>()
                        );
                        return;
                    default:
                        throw new NotSupportedException($"Specified indexer notification type ['{type}'] is not supported.");
                }
            }
            else
            {
                throw new FormatException("Indexer notification doesn't contain $type filed.");
            }
        }
        
        public void StartListening(Action<IndexerNotificationReceived> handleIndexerNotificationAction)
        {
            var consumer = new EventingBasicConsumer(_channel);
            
            consumer.Received += (sender, args) =>
            {
                handleIndexerNotificationAction(new IndexerNotificationReceived(
                    notificationBody: args.Body
                ));
            };

            _channel.BasicConsume
            ( 
                consumer:    consumer, 
                queue:       _options.NotificationsQueue,
                autoAck:     true,
                consumerTag: ConsumerTag
            );
        }

        public void StopListening()
        {
            _channel.BasicCancel(ConsumerTag);
        }
        
    }
}