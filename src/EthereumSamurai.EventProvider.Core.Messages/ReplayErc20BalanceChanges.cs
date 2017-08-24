﻿namespace EthereumSamurai.EventProvider.Core.Messages
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

        public ReplayErc20BalanceChanges(string exchange, string routingKey, int replayNumber, string assetHolder)
            : this(exchange, routingKey, assetHolder, Enumerable.Empty<string>())
        {
            ReplayNumber = replayNumber;
        }


        public string AssetHolder { get; }

        public ImmutableArray<string> Contracts { get; }

        public string Exchange { get; }

        public int? ReplayNumber { get; }

        public string RoutingKey { get; }
    }
}