namespace EthereumSamurai.EventProvider.Core.Actors
{
    using System;
    using System.Text;
    using Akka.Actor;
    using Akka.Event;
    using Messages;
    using Newtonsoft.Json;
    using RabbitMQ.Client;



    public sealed class SubscribersNotifierActor : ReceiveActor
    {
        private readonly IModel          _channel;
        private readonly ILoggingAdapter _logger;

        public SubscribersNotifierActor(IModel channel)
        {
            _channel = channel;
            _logger  = Context.GetLogger();

            Receive<Notify>(
                msg => Process(msg));
        }
        
        private void Process(Notify message)
        {
            try
            {
                var notificationJson = JsonConvert.SerializeObject(message.Notification);

                _channel.BasicPublish
                (
                    exchange:   message.Exchange,
                    routingKey: message.RoutingKey,
                    body:       Encoding.UTF8.GetBytes(notificationJson)
                );
            }
            catch (Exception e)
            {
                _logger.Error
                (
                    e,
                    $"Failed to send notification of type [{message.Notification?.GetType()}] to exchange [{message.Exchange}] with routing key [{message.RoutingKey}]"
                );
            }
        }
    }
}