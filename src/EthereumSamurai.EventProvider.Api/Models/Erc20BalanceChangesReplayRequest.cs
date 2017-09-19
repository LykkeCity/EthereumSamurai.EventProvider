namespace EthereumSamurai.EventProvider.Api.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Validation;


    /// <summary>
    ///    Erc20 balance changes replay request
    /// </summary>
    public class Erc20BalanceChangesReplayRequest
    {
        /// <summary>
        ///    Get balances for this asset holder address
        /// </summary>
        [Required, EthereumAddress]
        public string AssetHolder { get; set; }

        /// <summary>
        ///    Get balances for these contracts (leave null or empty, to get balances for all contracts)
        /// </summary>
        [EthereumAddressList]
        public IEnumerable<string> Contracts { get; set; }

        /// <summary>
        ///    Send balances to this exchange
        /// </summary>
        [Required]
        public string Exchange { get; set; }

        /// <summary>
        ///    Send a Erc20BalanceChangeReplayEndNotification message with this request id when finished request
        /// </summary>
        [Required]
        public int ReplayId { get; set; }

        /// <summary>
        ///    Send balances with this routing key
        /// </summary>
        [Required]
        public string RoutingKey { get; set; }
    }
}