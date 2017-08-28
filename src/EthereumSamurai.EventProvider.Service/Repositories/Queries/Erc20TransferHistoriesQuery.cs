namespace EthereumSamurai.EventProvider.Service.Repositories.Queries
{
    public sealed class Erc20TransferHistoriesQuery
    {
        public ulong? BlockNumber { get; set; }

        public string[] Contracts { get; set; }
        
        public ulong? FromBlockNumber { get; set; }

        public string[] AssetHolders { get; set; }
        
        public ulong? ToBlockNumber { get; set; }
    }
}