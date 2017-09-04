namespace EthereumSamurai.EventProvider.Notifications
{
    using Interfaces;


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
            int?   replayId,
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
            ReplayId         = replayId;
            To               = to;
            TransactionHash  = transactionHash;
            TransactionIndex = transactionIndex;
            TransferAmount   = transferAmount;
        }


        /// <inheritdoc />
        public string Type
            => "Erc20TransferCommit";

        /// <summary>
        ///    Hash of the block, containing the transfer.
        /// </summary>
        public string BlockHash { get; }

        /// <summary>
        ///    Number of the block, containing the transfer.
        /// </summary>
        public ulong BlockNumber { get; }

        /// <summary>
        ///    Timestamp of the block, containing the transfer.
        /// </summary>
        public ulong BlockTimestamp { get; }

        /// <summary>
        ///    Address of the related ERC20 contracr.
        /// </summary>
        public string ContractAddress { get; }

        /// <summary>
        ///    Transferor address.
        /// </summary>
        public string From { get; }

        /// <summary>
        ///    Index of the transfer log in the block.
        /// </summary>
        public uint LogIndex { get; }
        

        public int? ReplayId { get; }

        /// <summary>
        ///    Transferee address.
        /// </summary>
        public string To { get; }

        /// <summary>
        ///    Hash of the transaction, containing the transfer.
        /// </summary>
        public string TransactionHash { get; }

        /// <summary>
        ///    Index of the transaction, containing the transfer.
        /// </summary>
        public uint TransactionIndex { get; }

        /// <summary>
        ///    Amount of tokens being transferred.
        /// </summary>
        public string TransferAmount { get; }
    }
}