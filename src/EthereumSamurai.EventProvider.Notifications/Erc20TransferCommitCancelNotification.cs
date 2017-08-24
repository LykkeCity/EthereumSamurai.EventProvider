namespace EthereumSamurai.EventProvider.Notifications
{
    public sealed class Erc20TransferCommitCancelNotification : INotification
    {
        public Erc20TransferCommitCancelNotification(ulong blockNumber)
        {
            BlockNumber = blockNumber;
        }


        public string Type
            => "Erc20TransferCommitCancel";

        public ulong BlockNumber { get; }
    }
}