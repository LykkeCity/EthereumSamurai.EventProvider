namespace EthereumSamurai.EventProvider.Notifications
{
    using Interfaces;

    /// <summary>
    ///    Represents the ERC20 transfer commit cancellation notification.
    /// </summary>
    public sealed class Erc20TransferCommitCancelNotification : INotification
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="Erc20TransferCommitCancelNotification" /> class.
        /// </summary>
        public Erc20TransferCommitCancelNotification(
            ulong blockNumber)
        {
            BlockNumber = blockNumber;
        }

        /// <inheritdoc />
        public string Type
            => "Erc20TransferCommitCancel";

        /// <summary>
        ///    All ERC20 transfers in specified block should be treated as stale.
        /// </summary>
        public ulong BlockNumber { get; }
    }
}