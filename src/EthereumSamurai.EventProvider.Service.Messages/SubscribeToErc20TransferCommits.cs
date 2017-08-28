namespace EthereumSamurai.EventProvider.Service.Messages
{
    using System.Collections.Generic;
    using System.Collections.Immutable;

    public sealed class SubscribeToErc20TransferCommits
    {
        public SubscribeToErc20TransferCommits(string exchange, string routingKey, string assetHolder, IEnumerable<string> contracts)
        {
            AssetHolder = assetHolder;
            Contracts   = contracts.ToImmutableArray();
            Exchange    = exchange;
            RoutingKey  = routingKey;
        }

        public string AssetHolder { get; }

        public ImmutableArray<string> Contracts { get; }

        public string Exchange { get; }

        public string RoutingKey { get; }
    }
}