namespace EthereumSamurai.EventProvider.Api.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Validation;


    public class Erc20BalanceChangesSubscription
    {
        [Required]
        public string AssetHolder { get; set; }

        [EthereumAddressList]
        public IEnumerable<string> Contracts { get; set; }

        [Required]
        public string Exchange { get; set; }

        [Required]
        public string RoutingKey { get; set; }
    }
}