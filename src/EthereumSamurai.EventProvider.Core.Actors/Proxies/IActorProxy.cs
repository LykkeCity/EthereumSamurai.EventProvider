namespace EthereumSamurai.EventProvider.Core.Actors.Proxies
{
    using System;
    using System.Threading.Tasks;

    public interface IActorProxy
    {
        Task<object> Ask(object message, TimeSpan timeout);

        Task<T> Ask<T>(object message, TimeSpan timeout);

        void Tell(object message);
    }
}