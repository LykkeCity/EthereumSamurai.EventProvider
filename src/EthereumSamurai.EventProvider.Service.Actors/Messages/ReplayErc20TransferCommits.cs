namespace EthereumSamurai.EventProvider.Service.Actors.Messages
{
    using System.Collections.Generic;
    using System.Collections.Immutable;


    public sealed class ReplayErc20TransferCommits
    {
        internal ReplayErc20TransferCommits(string exchange, string routingKey, string assetHolder, IEnumerable<string> contracts)
        {
            AssetHolder = assetHolder;
            Contracts   = contracts.ToImmutableArray();
            Exchange    = exchange;
            RoutingKey  = routingKey;
        }

        public ReplayErc20TransferCommits(string exchange, string routingKey, int replayId, string assetHolder, IEnumerable<string> contracts)
            : this(exchange, routingKey, assetHolder, contracts)
        {
            ReplayId = replayId;
        }


        public string AssetHolder { get; }

        public ImmutableArray<string> Contracts { get; }

        public string Exchange { get; }

        public int? ReplayId { get; }

        public string RoutingKey { get; }
    }
}