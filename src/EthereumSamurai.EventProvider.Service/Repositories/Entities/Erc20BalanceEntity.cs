namespace EthereumSamurai.EventProvider.Service.Repositories.Entities
{
    public class Erc20BalanceEntity
    {
        public string AssetHolderAddress { get; set; }
        
        public string Balance { get; set; }
        
        public ulong BlockNumber { get; set; }
        
        public string ContractAddress { get; set; }
    }
}