namespace EthereumSamurai.EventProvider.Notifications
{
    public class Erc20TransferCommitNotification : INotification
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="Erc20TransferCommitNotification" /> class.
        /// </summary>
        public Erc20TransferCommitNotification(
            string blockHash,
            ulong  blockNumber,
            ulong  blockTimestamp,
            string contractAddress,
            string from,
            uint   logIndex,
            int?   replayNumber,
            string to,
            string transactionHash,
            uint   transactionIndex,
            string transferAmount)
        {
            BlockHash        = blockHash;
            BlockNumber      = blockNumber;
            BlockTimestamp   = blockTimestamp;
            ContractAddress  = contractAddress;
            From             = from;
            LogIndex         = logIndex;
            ReplayNumber     = replayNumber;
            To               = to;
            TransactionHash  = transactionHash;
            TransactionIndex = transactionIndex;
            TransferAmount   = transferAmount;
        }


        /// <inheritdoc />
        public string Type
            => "Erc20TransferCommit";

        public string BlockHash { get; }
        
        public ulong BlockNumber { get; }
        
        public ulong BlockTimestamp { get; }
        
        public string ContractAddress { get; }
        
        public string From { get; }
        
        public uint LogIndex { get; }
        
        public int? ReplayNumber { get; }

        public string To { get; }
        
        public string TransactionHash { get; }
        
        public uint TransactionIndex { get; }
        
        public string TransferAmount { get; }
    }
}