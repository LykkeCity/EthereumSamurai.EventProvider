namespace EthereumSamurai.EventProvider.Core.Messages
{
    public sealed class BlockBalancesIndexed
    {
        public BlockBalancesIndexed(ulong blockNumber)
        {
            BlockNumber = blockNumber;
        }


        public ulong BlockNumber { get; }
    }
}