using EthereumSamurai.EventProvider.Service.Messages;

namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors
{
    using Messages;

    public interface ISubscriberNotifierBehavior
    {
        void Process(Notify message);
    }
}