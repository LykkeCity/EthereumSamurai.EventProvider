namespace EthereumSamurai.EventProvider.Service.Repositories.Entities.Interfaces
{
    public interface IErc20SubscriptionEntity
    {
        string AssetHolder { get; set; }
        
        string Contract { get; set; }
        
        string Subscriber { get; set; }
    }
}