namespace EthereumSamurai.EventProvider.Core.Messages
{
    using System.Collections.Generic;
    using System.Collections.Immutable;

    public class UnsubscribeFromErc20BalanceChanges
    {
        public UnsubscribeFromErc20BalanceChanges(string exchange, string routingKey, string assetHolder, IEnumerable<string> contracts)
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