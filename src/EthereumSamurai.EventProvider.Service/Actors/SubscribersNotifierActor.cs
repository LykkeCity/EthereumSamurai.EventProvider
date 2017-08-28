namespace EthereumSamurai.EventProvider.Service.Actors
{
    using System;
    using Akka.Actor;
    using Akka.Event;
    using Behaviors;
    using Messages;



    public sealed class SubscribersNotifierActor : ReceiveActor
    {

        private readonly ISubscriberNotifierBehavior _behavior;
        private readonly ILoggingAdapter             _logger;

        public SubscribersNotifierActor(
            ISubscriberNotifierBehavior behavior)
        {
            _behavior = behavior;
            _logger   = Context.GetLogger();

            Receive<Notify>(
                msg => Process(msg));
        }
        
        private void Process(Notify message)
        {
            try
            {
                _behavior.Process(message);
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