namespace EthereumSamurai.EventProvider.Service.Messages
{
    using Newtonsoft.Json;

    public sealed class BlockBalancesIndexed
    {
        [JsonConstructor]
        public BlockBalancesIndexed(ulong blockNumber)
        {
            BlockNumber = blockNumber;
        }


        public ulong BlockNumber { get; }
    }
}