namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using System.Text;
    using Interfaces;
    using Messages;
    using Newtonsoft.Json;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Exceptions;


    internal sealed class SubscriberNotifierBehavior : ISubscriberNotifierBehavior
    {
        private readonly IModel _channel;


        public SubscriberNotifierBehavior(IModel channel)
        {
            _channel = channel;
        }

        public void Process(Notify message)
        {
            var notificationJson = JsonConvert.SerializeObject(message.Notification);
            var notification     = Encoding.UTF8.GetBytes(notificationJson);

            try
            {
                _channel.BasicPublish
                (
                    exchange:   message.Exchange,
                    routingKey: message.RoutingKey,
                    body:       notification
                );
            }
            catch (AlreadyClosedException e) when (e.ShutdownReason?.ReplyCode == 404)
            {
                // Exchange, specified in subscription, not found. 
                // TODO: Log this as warning 
            }
        }
    }
}