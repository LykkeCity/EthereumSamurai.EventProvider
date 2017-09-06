namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors.Factories.Interfaces
{
    using RabbitMQ.Client;
    using Repositories.Factories;

    public interface IChannelFactory
    {
        IModel GetChannel(ChannelType type);
    }
}