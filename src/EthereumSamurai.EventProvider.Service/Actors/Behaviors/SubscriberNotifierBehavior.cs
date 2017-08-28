namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using System.Text;
    using Messages;
    using Newtonsoft.Json;
    using RabbitMQ.Client;


    
    public sealed class SubscriberNotifierBehavior : ISubscriberNotifierBehavior
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

            _channel.BasicPublish
            (
                exchange:   message.Exchange,
                routingKey: message.RoutingKey,
                body:       notification
            );
        }
    }
}