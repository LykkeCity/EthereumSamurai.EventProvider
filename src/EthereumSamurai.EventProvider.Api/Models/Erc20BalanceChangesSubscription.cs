namespace EthereumSamurai.EventProvider.Api.Models
{
    using System.Collections.Generic;

    public class Erc20BalanceChangesSubscription
    {
        public string AssetHolder { get; set; }

        public IEnumerable<string> Contracts { get; set; }

        public string Exchange { get; set; }

        public string RoutingKey { get; set; }
    }
}