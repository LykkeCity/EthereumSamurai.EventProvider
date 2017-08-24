namespace EthereumSamurai.EventProvider.Notifications
{
    public class Erc20TransferCommitReplayEndNotification : INotification
    {
        public Erc20TransferCommitReplayEndNotification(int replayNumber)
        {
            ReplayNumber = replayNumber;
        }



        public string Type
            => "Erc20TransferCommitReplayEnd";

        public int ReplayNumber { get; }
    }
}