﻿namespace EthereumSamurai.EventProvider.Service.Actors.Messages
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    public class ReplayErc20BalanceChanges
    {
        internal ReplayErc20BalanceChanges(string exchange, string routingKey, string assetHolder, IEnumerable<string> contracts)
        {
            AssetHolder = assetHolder;
            Contracts   = contracts.ToImmutableArray();
            Exchange    = exchange;
            RoutingKey  = routingKey;
        }

        public ReplayErc20BalanceChanges(string exchange, string routingKey, int replayId, string assetHolder)
            : this(exchange, routingKey, assetHolder, Enumerable.Empty<string>())
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