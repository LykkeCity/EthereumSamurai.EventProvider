using EthereumSamurai.EventProvider.Core.Messages;

namespace EthereumSamurai.EventProvider.Core.Actors.Behaviors
{
    public interface ISubscriberNotifierBehavior
    {
        void Process(Notify message);
    }
}