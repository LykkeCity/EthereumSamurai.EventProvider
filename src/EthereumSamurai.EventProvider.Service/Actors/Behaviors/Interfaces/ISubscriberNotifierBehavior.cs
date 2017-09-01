namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors.Interfaces
{
    using Messages;

    internal interface ISubscriberNotifierBehavior
    {
        void Process(Notify message);
    }
}