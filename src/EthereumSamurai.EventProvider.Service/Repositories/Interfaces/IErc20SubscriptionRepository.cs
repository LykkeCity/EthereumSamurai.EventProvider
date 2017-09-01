namespace EthereumSamurai.EventProvider.Service.Repositories.Interfaces
{
    using System.Collections.Generic;
    using Entities.Interfaces;


    public interface IErc20SubscriptionRepository<T>
        where T : IErc20SubscriptionEntity
    {
        string CollectionName { get; }

        IEnumerable<(string exchange, string routingKey)> GetSubscribers();
        
        IEnumerable<(string exchange, string routingKey)> GetSubscribers(string contract, params string[] assetHolders);

        void Subscribe(string exchange, string routingKey, string assetHolder = "*", string contract = "*");
        
        void Unsubscribe(string exchange, string routingKey, string assetHolder = "*", string contract = "*");
    }
}