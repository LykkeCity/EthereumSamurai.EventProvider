namespace EthereumSamurai.EventProvider.Notifications
{
    public sealed class Erc20BalanceChangeNotification : INotification
    {
        public Erc20BalanceChangeNotification(string assetHolderAddress, string balance, ulong blockNumber, string contractAddress, int? replayNumber)
        {
            AssetHolderAddress = assetHolderAddress;
            Balance            = balance;
            BlockNumber        = blockNumber;
            ContractAddress    = contractAddress;
            ReplayNumber       = replayNumber;
        }




        public string Type
            => "Erc20BalanceChange";

        public string AssetHolderAddress { get; }
        
        public string Balance { get; }
        
        public ulong BlockNumber { get; }
        
        public string ContractAddress { get; }

        public int? ReplayNumber { get; }
    }
}