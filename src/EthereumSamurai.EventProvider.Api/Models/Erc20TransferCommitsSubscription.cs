namespace EthereumSamurai.EventProvider.Api.Models
{
    using System.Collections.Generic;

    public class Erc20TransferCommitsSubscription
    {
        public string AssetHolder { get; set; }

        public IEnumerable<string> Contracts { get; set; }

        public string Exchange { get; set; }

        public string RoutingKey { get; set; }
    }
}