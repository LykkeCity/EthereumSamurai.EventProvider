namespace EthereumSamurai.EventProvider.Notifications
{
    using Interfaces;

    public class Erc20TransferCommitReplayEndNotification : INotification
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="Erc20TransferCommitReplayEndNotification" /> class.
        /// </summary>
        public Erc20TransferCommitReplayEndNotification(int replayId)
        {
            ReplayId = replayId;
        }


        /// <inheritdoc />
        public string Type
            => "Erc20TransferCommitReplayEnd";

        /// <summary>
        ///    Id of the replay, to mark <see cref="Erc20TransferCommitNotification"/> instances.
        /// </summary>
        public int ReplayId { get; }
    }
}