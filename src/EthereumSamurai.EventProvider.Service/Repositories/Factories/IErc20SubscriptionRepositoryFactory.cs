namespace EthereumSamurai.EventProvider.Service.Repositories.Factories
{
    using System;

    public interface IErc20SubscriptionRepositoryFactory
    {
        IErc20SubscriptionRepository GetRepository(string collectionName);

        IErc20SubscriptionRepository GetRepositoryWithCache(string collectionName, TimeSpan cacheDuration);
    }
}