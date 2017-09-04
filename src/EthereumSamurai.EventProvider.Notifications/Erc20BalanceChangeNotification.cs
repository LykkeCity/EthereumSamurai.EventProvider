namespace EthereumSamurai.EventProvider.Notifications
{
    using Interfaces;

    /// <summary>
    ///    Represents the ERC20 balance change notification.
    /// </summary>
    public sealed class Erc20BalanceChangeNotification : INotification
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="Erc20BalanceChangeNotification" /> class.
        /// </summary>
        public Erc20BalanceChangeNotification(
            string assetHolderAddress,
            string balance,
            ulong  blockNumber,
            string contractAddress,
            int?   replayId)
        {
            AssetHolderAddress = assetHolderAddress;
            Balance            = balance;
            BlockNumber        = blockNumber;
            ContractAddress    = contractAddress;
            ReplayId           = replayId;
        }

        
        /// <inheritdoc />
        public string Type
            => "Erc20BalanceChange";

        /// <summary>
        ///    Address of asset holder
        /// </summary>
        public string AssetHolderAddress { get; }
        
        /// <summary>
        ///    New value of balance
        /// </summary>
        public string Balance { get; }
        
        /// <summary>
        ///    
        /// </summary>
        public ulong BlockNumber { get; }
        
        /// <summary>
        ///    Address of ERC20 contract.
        /// </summary>
        public string ContractAddress { get; }

        /// <summary>
        ///    Id of replay, specified in an API replay request.
        /// </summary>
        public int? ReplayId { get; }
    }
}