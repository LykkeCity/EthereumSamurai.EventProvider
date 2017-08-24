namespace EthereumSamurai.EventProvider.Notifications
{
    public sealed class Erc20BalanceChangeCancelNotification : INotification
    {
        public Erc20BalanceChangeCancelNotification(ulong fromBlockNumber)
        {
            FromBlockNumber = fromBlockNumber;
        }



        public string Type
            => "Erc20BalanceChangeCancel";

        public ulong FromBlockNumber { get; }
    }
}