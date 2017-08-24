namespace EthereumSamurai.EventProvider.Core.Messages
{
    public class BlockIndexed
    {
        public BlockIndexed(ulong blockNumber)
        {
            BlockNumber = blockNumber;
        }


        public ulong BlockNumber { get; }
    }
}