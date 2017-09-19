namespace EthereumSamurai.EventProvider.Api.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Validation;


    /// <summary>
    ///    Erc20 transfer commits subscription
    /// </summary>
    public class Erc20TransferCommitsSubscription
    {
        /// <summary>
        ///    Subscribe to/unsubscribe from transfer commits of this asset holder address
        /// </summary>
        [Required, EthereumAddress]
        public string AssetHolder { get; set; }

        /// <summary>
        ///    Subscribe to/unsubscribe from transfer commits of these contracts (leave null or empty, to subscribe to/unsubscribe from transfer commits of all contracts)
        /// </summary>
        [EthereumAddressList]
        public IEnumerable<string> Contracts { get; set; }

        /// <summary>
        ///    Send transfer commits to this exchange
        /// </summary>
        [Required]
        public string Exchange { get; set; }

        /// <summary>
        ///    Send transfer commits with this routing key
        /// </summary>
        [Required]
        public string RoutingKey { get; set; }
    }
}