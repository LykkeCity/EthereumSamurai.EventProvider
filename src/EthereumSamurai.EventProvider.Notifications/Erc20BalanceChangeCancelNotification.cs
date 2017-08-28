namespace EthereumSamurai.EventProvider.Notifications
{
    /// <summary>
    ///    Represents the ERC20 balance change cancellation notification.
    /// </summary>
    public sealed class Erc20BalanceChangeCancelNotification : INotification
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="Erc20BalanceChangeCancelNotification" /> class.
        /// </summary>
        public Erc20BalanceChangeCancelNotification(ulong fromBlockNumber)
        {
            FromBlockNumber = fromBlockNumber;
        }
        
        /// <inheritdoc />
        public string Type
            => "Erc20BalanceChangeCancel";

        /// <summary>
        ///    All balances from specified block and higher should be treated as stale.
        /// </summary>
        public ulong FromBlockNumber { get; }
    }
}