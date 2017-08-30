namespace EthereumSamurai.EventProvider.Service.Repositories.Factories
{
    using Subscriptions;

    public interface IErc20SubscriptionRepositoryFactory
    {
        IErc20SubscriptionRepository<T> GetRepository<T>(string collectionName)
            where T : IErc20Subscription;
    }
}