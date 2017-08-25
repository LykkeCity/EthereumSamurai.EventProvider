namespace EthereumSamurai.EventProvider.Core.Actors.Behaviors
{
    using System;
    using System.Linq;
    using System.Text;
    using Messages;
    using Newtonsoft.Json.Linq;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public class IndexerNotificationListenerBehavior : IIndexerNotificationListenerBehavior
    {
        private const string ConsumerTag = "IndexerNotificationListener";

        private readonly IModel         _channel;
        private readonly IConfiguration _configuration;
        
        public IndexerNotificationListenerBehavior(
            IModel channel,
            IConfiguration configuration)
        {
            _channel       = channel;
            _configuration = configuration;
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
                queue:       _configuration.IndexerNotificationsQueue,
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