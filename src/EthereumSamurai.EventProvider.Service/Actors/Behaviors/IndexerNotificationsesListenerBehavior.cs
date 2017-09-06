namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using System;
    using System.Linq;
    using System.Text;
    using Interfaces;
    using Messages;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json.Linq;
    using Options;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;


    internal class IndexerNotificationsesListenerBehavior : IIndexerNotificationsListenerBehavior
    {
        private const string ConsumerTag = "IndexerNotificationsListener";

        private readonly IModel                             _channel;
        private readonly IndexerNotificationListenerOptions _options;


        public IndexerNotificationsesListenerBehavior(
            IModel channel,
            IOptions<IndexerNotificationListenerOptions> options)
        {
            _channel = channel;
            _options = options.Value;
        }
        
        public void Process(
            IndexerNotificationReceived  message,
            Action<BlockBalancesIndexed> notifyAboutIndexedBlockBalancesAction,
            Action<BlockIndexed>         notifyAboutIndexedBlockAction)
        {
            var notificationJson = Encoding.UTF8.GetString(message.NotificationBody.ToArray());
            var notification     = JObject.Parse(notificationJson);
            
            if (notification.TryGetValue("IndexingMessageType",  out var typeToken) && 
                notification.TryGetValue("BlockNumber",          out var blockNumberToken)  &&
                ulong.TryParse(blockNumberToken.Value<string>(), out var blockNumber))
            {
                var type = typeToken.Value<string>();
                
                switch (type.ToLowerInvariant())
                {
                    case "block":
                        notifyAboutIndexedBlockAction
                        (
                            new BlockIndexed(blockNumber)
                        );
                        return;
                    case "ercbalances":
                        notifyAboutIndexedBlockBalancesAction
                        (
                            new BlockBalancesIndexed(blockNumber)
                        );
                        return;
                    default:
                        throw new NotSupportedException($"Specified indexer notification type ['{type}'] is not supported.");
                }
            }
            else
            {
                throw new FormatException("RabbitIndexingMessage either not contains IndexingMessageType, or BlockNumber in proper format.");
            }
        }
        
        public void StartListening(Action<IndexerNotificationReceived> handleIndexerNotificationAction)
        {
            _channel.QueueDeclare
            (
                queue:      _options.NotificationsQueue,
                durable:    true,
                exclusive:  false,
                autoDelete: false
            );

            _channel.QueueBind
            (
                queue:      _options.NotificationsQueue,
                exchange:   _options.NotificationsExchange,
                routingKey: string.Empty
            );

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