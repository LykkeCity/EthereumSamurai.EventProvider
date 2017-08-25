namespace EthereumSamurai.EventProvider.Core.Messages
{
    using Newtonsoft.Json;

    public sealed class BlockIndexed
    {
        [JsonConstructor]
        public BlockIndexed(ulong blockNumber)
        {
            BlockNumber = blockNumber;
        }


        public ulong BlockNumber { get; }
    }
}