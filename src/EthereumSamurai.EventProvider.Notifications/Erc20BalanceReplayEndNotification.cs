namespace EthereumSamurai.EventProvider.Notifications
{
    public class Erc20BalanceChangeReplayEndNotification : INotification
    {
        public Erc20BalanceChangeReplayEndNotification(int replayNumber)
        {
            ReplayNumber = replayNumber;
        }



        public string Type
            => "Erc20BalanceChangeReplayEnd";

        public int ReplayNumber { get; }
    }
}