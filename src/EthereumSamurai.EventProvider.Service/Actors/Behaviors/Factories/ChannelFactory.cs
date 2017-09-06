namespace EthereumSamurai.EventProvider.Service.Actors.Behaviors.Factories
{
    using System;
    using Interfaces;
    using Microsoft.Extensions.Configuration;
    using RabbitMQ.Client;
    using Repositories.Factories;
    

    public class ChannelFactory : IChannelFactory
    {
        private readonly IConfigurationRoot _configuration;

        public ChannelFactory(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }


        public IModel GetChannel(ChannelType type)
        {
            string connectionStringName;

            switch (type)
            {
                case ChannelType.Incoming:
                    connectionStringName = "IncominRabbitMQ";
                    break;
                case ChannelType.Outgoing:
                    connectionStringName = "OutgoingRabbitMQ";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Unexpected channel type.");
            }

            var connectionString = _configuration.GetConnectionString(connectionStringName);
            var rabbitUri        = new Uri(connectionString);

            var connectionFactory = new ConnectionFactory
            {
                AutomaticRecoveryEnabled = true,
                Uri                      = rabbitUri
            };

            var connection = connectionFactory.CreateConnection();

            return connection.CreateModel();
        }
    }
}