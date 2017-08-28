namespace EthereumSamurai.EventProvider.Service.Repositories.Queries
{
    public sealed class Erc20BalancesQuery
    {
        public string AssetHolder { get; set; }
        
        public ulong? BlockNumber { get; set; }

        public string[] Contracts { get; set; }
    }
}