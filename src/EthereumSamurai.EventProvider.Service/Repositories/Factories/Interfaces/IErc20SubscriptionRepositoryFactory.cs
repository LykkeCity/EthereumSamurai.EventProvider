namespace EthereumSamurai.EventProvider.Service.Repositories.Factories.Interfaces
{
    using Entities.Interfaces;
    using Repositories.Interfaces;


    public interface IErc20SubscriptionRepositoryFactory
    {
        IErc20SubscriptionRepository<T> GetRepository<T>(string collectionName)
            where T : IErc20SubscriptionEntity, new();
    }
}