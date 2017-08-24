namespace EthereumSamurai.EventProvider.Core.Repositories
{
    using System.Collections.Generic;

    public interface IErc20SubscriptionRepository
    {
        string CollectionName { get; }

        IEnumerable<(string exchange, string routingKey)> GetSubscribers();

        IEnumerable<(string exchange, string routingKey)> GetSubscribers(string assetHolder, string contract);

        IEnumerable<(string exchange, string routingKey)> GetSubscribers(string assetHolder, string[] contracts);

        IEnumerable<(string exchange, string routingKey)> GetSubscribers(string[] assetHolders, string contract);
        
        void Subscribe(string exchange, string routingKey, string assetHolder = "*", string contract = "*");
        
        void Unsubscribe(string exchange, string routingKey, string assetHolder = "*", string contract = "*");
    }
}