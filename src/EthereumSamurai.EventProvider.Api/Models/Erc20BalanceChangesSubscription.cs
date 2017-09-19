namespace EthereumSamurai.EventProvider.Api.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Validation;


    /// <summary>
    ///    Erc20 balance changes subscription
    /// </summary>
    public class Erc20BalanceChangesSubscription
    {
        /// <summary>
        ///    Subscribe to/unsubscribe from balance changes of this asset holder address
        /// </summary>
        [Required, EthereumAddress]
        public string AssetHolder { get; set; }

        /// <summary>
        ///    Subscribe to/unsubscribe from balance changes of these contracts (leave null or empty, to subscribe to/unsubscribe from balance changes of all contracts)
        /// </summary>
        [EthereumAddressList]
        public IEnumerable<string> Contracts { get; set; }

        /// <summary>
        ///    Send balance changes to this exchange
        /// </summary>
        [Required]
        public string Exchange { get; set; }

        /// <summary>
        ///    Send balance changes with this routing key
        /// </summary>
        [Required]
        public string RoutingKey { get; set; }
    }
}