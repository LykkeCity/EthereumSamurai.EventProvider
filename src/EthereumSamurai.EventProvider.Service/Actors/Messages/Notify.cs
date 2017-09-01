namespace EthereumSamurai.EventProvider.Service.Actors.Messages
{
    using Akka.Routing;
    using Notifications.Interfaces;

    

    internal sealed class Notify : IConsistentHashable
    {
        internal Notify(string exchange, string routingKey, INotification notification)
        {
            ConsistentHashKey = CalculateConsistenHashKey(exchange, routingKey);
            Exchange          = exchange;
            Notification      = notification;
            RoutingKey        = routingKey;
        }


        public object ConsistentHashKey { get; }

        public string Exchange { get; }

        public INotification Notification { get; }

        public string RoutingKey { get; }
        

        private static object CalculateConsistenHashKey(string exchange, string routingKey)
        {
            unchecked
            {
                var hash = 17;

                hash = hash * 23 + exchange.GetHashCode();
                hash = hash * 23 + routingKey.GetHashCode();

                return hash;
            }
        }
    }
}