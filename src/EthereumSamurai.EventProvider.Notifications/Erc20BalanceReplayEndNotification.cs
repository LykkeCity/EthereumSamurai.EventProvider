namespace EthereumSamurai.EventProvider.Notifications
{
    /// <summary>
    ///    Represents the completion of ERC20 balance changes trplay notification.
    /// </summary>
    public class Erc20BalanceChangeReplayEndNotification : INotification
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="Erc20BalanceChangeReplayEndNotification" /> class.
        /// </summary>
        public Erc20BalanceChangeReplayEndNotification(
            int replayNumber)
        {
            ReplayNumber = replayNumber;
        }


        /// <inheritdoc />
        public string Type
            => "Erc20BalanceChangeReplayEnd";

        public int ReplayNumber { get; }
    }
}